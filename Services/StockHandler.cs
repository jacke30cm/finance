using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;

namespace Services
{
   public class StockHandler
    {




       
       public static string GetStockQuotes(string url)
       {

           HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
           HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

           StreamReader sr = new StreamReader(resp.GetResponseStream());
           string results = sr.ReadToEnd();
           sr.Close();

           return results;
       }

       public void PopulateStockQuotes()
       {

           string[] lines = File.ReadAllLines(@"C:\StockTickersSverige.txt");

           string builder = "";

           foreach (var line in lines)
           {

               builder += line + "+";

           }


           // StockSymbols
           // n = Name, s = ticker, x = Market, p2 = changeinpercent, c1 = changeincash, b = bid, a = ask, l1 = lasttrade, h = dayshighest, g=dayslowest 

           int counter = 0;

           string fileList = GetStockQuotes("http://finance.yahoo.com/d/quotes.csv?s=" + builder + "&f=nsxp2c1bal1hg");
           string[] tempStr;
           var Share = new Share();
           var ShareHistory = new ShareHistory();
           tempStr = fileList.Split(',', '\n');

           for (int i = 0; i < tempStr.Length; i++)
           {
               if (!string.IsNullOrWhiteSpace(tempStr[i]))
               {
                   if (counter == 0)
                   {
                       StringBuilder sb = new StringBuilder();
                       foreach (char c in tempStr[i])
                       {
                           if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                           {
                               sb.Append(c);
                           }
                       }


                       Share.Name = sb.ToString();
                   }
                   else if (counter == 1)
                   {

                       Share.Ticker = tempStr[i];
                   }
                   else if (counter == 2)
                   {
                       Share.Market = tempStr[i];
                   }
                   else if (counter == 3)
                   {
                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           var newcomma = stockwithcomma.Remove(stockwithcomma.Length - 2).Remove(0,1);
                           
                          ShareHistory.ChangePercent = Convert.ToDouble(newcomma);
                       }
                       else
                       {
                           ShareHistory.ChangePercent = Convert.ToDouble(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 4)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.ChangeCash = Convert.ToDecimal(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.ChangeCash = Convert.ToDecimal(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 5)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Bid = Convert.ToDecimal(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Bid = Convert.ToDecimal(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 6)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Ask = Convert.ToDecimal(stockwithcomma);
                       }
                       else if (tempStr[i].Equals("N/A"))
                       {
                           ShareHistory.Ask = 0;
                       }
                       else
                       {
                           ShareHistory.Ask = Convert.ToDecimal(tempStr[i]);
                       }

                       
                   }
                   else if (counter == 7)
                   {
                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Latest = Convert.ToDecimal(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Latest = Convert.ToDecimal(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 8)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Highest = Convert.ToDecimal(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Highest = Convert.ToDecimal(tempStr[i]);
                       }

                       
                   }
                   else if (counter == 9)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Lowest = Convert.ToDecimal(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Lowest = Convert.ToDecimal(tempStr[i]);
                       }

                      
                   }


                   
                   
               }
               if (counter == 9)
               {

                   Share.Market = "Large Cap";
                   Share.Description = "Bra och stabil aktie";
                   Share.Country = new Country() { Id = 1 };
                   ShareHistory.TimeStamp = DateTime.Now;
                   AddStockToDatabase(Share, ShareHistory);

                   counter = 0;
                   Share = new Share();
                   ShareHistory = new ShareHistory();
               }
               else
               {
                   counter++;
               }

           }


       }

       public void AddStockToDatabase(Share share, ShareHistory history)
       {

           var uow = new DataWorker();
           
           uow.ShareRepository.Add(share);
           uow.Save();

           var insertedShare = uow.ShareRepository.GetSingle(x => x.Ticker.Equals(share.Ticker));

           history.Share = insertedShare;
           uow.ShareHistoryRepository.Add(history);

           uow.Save();
       }

       public void UpdateDatbase()
       {
           var uow = new DataWorker();
           
           string[] tickers = File.ReadAllLines(@"C:\StockTickersSverige.txt");
           string[] names = File.ReadAllLines(@"C:\StockNames.txt");
           string[] cap = File.ReadAllLines(@"C:\StockCap.txt");


           for (int i = 0; i < tickers.Length; i++)
           {
               
               
             var share = uow.ShareRepository.Get(x => x.Ticker.Equals(tickers[i])).SingleOrDefault();

             share.Name = names[i];
             share.Market = cap[i];

             uow.ShareRepository.Update(share);
             uow.Save();

         
           }




       }





    }
}
