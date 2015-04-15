using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    
       
       public string GetStockQuotes(string url)
       {

           string results = "";

           try
           {

               HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
               HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

               StreamReader sr = new StreamReader(resp.GetResponseStream());
               results = sr.ReadToEnd();
               sr.Close();

               
           }
           catch (WebException e)
           {
               if (e.Status == WebExceptionStatus.ProtocolError)
               {
                  
                   RequestStockQuotes();
                   
               }
           }


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
           var uow = new DataWorker();
           var coun = new Country() { Name = "Sverige" };
           uow.CountryRepository.Add(coun);
           uow.Save();

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

                       Share.Ticker = tempStr[i].Replace("\"","");
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
                           ShareHistory.ChangeCash = Convert.ToDouble(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.ChangeCash = Convert.ToDouble(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 5)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Bid = Convert.ToDouble(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Bid = Convert.ToDouble(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 6)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Ask = Convert.ToDouble(stockwithcomma);
                       }
                       else if (tempStr[i].Equals("N/A"))
                       {
                           ShareHistory.Ask = 0;
                       }
                       else
                       {
                           ShareHistory.Ask = Convert.ToDouble(tempStr[i]);
                       }

                       

                   }
                   else if (counter == 7)
                   {
                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Latest = Convert.ToDouble(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Latest = Convert.ToDouble(tempStr[i]);
                       }
                       
                   }
                   else if (counter == 8)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Highest = Convert.ToDouble(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Highest = Convert.ToDouble(tempStr[i]);
                       }

                       
                   }
                   else if (counter == 9)
                   {

                       if (tempStr[i].Contains("."))
                       {
                           var stockwithcomma = tempStr[i].Replace(".", ",");
                           ShareHistory.Lowest = Convert.ToDouble(stockwithcomma);
                       }
                       else
                       {
                           ShareHistory.Lowest = Convert.ToDouble(tempStr[i]);
                       }

                      
                   }
                   
                   
               }
               if (counter == 9)
               {
                   
                   Share.Market = "Large Cap";
                   Share.Description = "Bra och stabil aktie";
                   ShareHistory.TimeStamp = DateTime.Now;
                   AddStockToDatabase(Share, ShareHistory);

                   counter = 0;
                  }
               else
               {
                   counter++;
               }

           }


       }

       public void RequestStockQuotes()
       {

           string[] lines = File.ReadAllLines(@"C:\StockTickersUSA.txt");


           string builder = "";

               //lines.Aggregate("", (current, line) => current + (line + "+"));

           for (int i = 0; i < lines.Length; i++)
           {

               if (i < lines.Length - 1)
               {

                   builder += lines[i] + "+";

               }
               else
               {
                   builder += lines[i];
               }
               

           }

           var b = "sd";
            //StockSymbols
            //s = ticker, p2 = changeinpercent, c1 = changeincash, b = bid, a = ask, l1 = lasttrade, h = dayshighest, g=dayslowest 

           string fileList = GetStockQuotes("http://finance.yahoo.com/d/quotes.csv?s=" + builder + "&f=sp2c1bal1hg");
           var share = new Share();
           var shareHistory = new ShareHistory(){TimeStamp = DateTime.Now};
           string[] tempStr = fileList.Split(',', '\n');
          
           int counter = 0;

           foreach (string t in tempStr)
           {
               if (!string.IsNullOrWhiteSpace(t))
               {
                   if (counter == 0)
                   {

                       share.Ticker = t.Replace("\"", "");
                       

                   }
                   else if (counter == 1)
                   {
                       var temp = t.Remove(t.Length - 2).Remove(0, 1).Replace(".", ",");
                       shareHistory.ChangePercent = Convert.ToDouble(temp);

                   }
                   else if (counter == 2)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.ChangeCash = Convert.ToDouble(temp);
                   }
                   else if (counter == 3)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.Bid = Convert.ToDouble(temp);
                       
                   }
                   else if (counter == 4)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.Ask = Convert.ToDouble(temp);
                       
                   }
                   else if (counter == 5)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.Latest = Convert.ToDouble(temp);
                       
                   }
                   else if (counter == 6)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.Highest = Convert.ToDouble(temp);

                   }
                   else if (counter == 7)
                   {
                       var temp = t.Replace(".", ",");
                       shareHistory.Lowest = Convert.ToDouble(temp);
                       
                   }
                
                   
               }
               if (counter == 7)
               {
               
                   InsertNewStockHistory(share, shareHistory);

                   counter = 0;
               }
               else
               {
                   counter++;
               }
           }
           
       
       }

       public void InsertNewStockHistory(Share share, ShareHistory history)
       {

           var uow = new DataWorker();

           history.Share = uow.ShareRepository.GetSingle(x => x.Ticker.Equals(share.Ticker));
           
           uow.ShareHistoryRepository.Add(history);

           uow.Save();


       }

       public void AddStockToDatabase(Share share, ShareHistory history)
       {

           var uow = new DataWorker();

           share.Country = uow.CountryRepository.GetSingle(x => x.Name.Equals("Sverige"));
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

           string[] tickers = File.ReadAllLines(@"C:\StockTickersSverige.txt", Encoding.GetEncoding("ISO-8859-15"));
           string[] names = File.ReadAllLines(@"C:\StockNames.txt", Encoding.GetEncoding("ISO-8859-15"));
           string[] cap = File.ReadAllLines(@"C:\StockCap.txt", Encoding.GetEncoding("ISO-8859-15"));


           for (int i = 0; i < tickers.Length; i++)
           {
               var tmp = tickers[i];
               
             var share = uow.ShareRepository.GetSingle(x => x.Ticker == tmp);

               

             share.Name = names[i];
             share.Market = cap[i];

             uow.ShareRepository.Update(share);
             uow.Save();

         
           }
           

       }

       public void AddUSAStock()
       {
           var uow = new DataWorker();

           string[] tickers = File.ReadAllLines(@"C:\StockTickersUSA.txt", Encoding.GetEncoding("ISO-8859-15"));
           string[] names = File.ReadAllLines(@"C:\StockNamesUSA.txt", Encoding.GetEncoding("ISO-8859-15"));
           string[] cap = File.ReadAllLines(@"C:\StockCapUSA.txt", Encoding.GetEncoding("ISO-8859-15"));

           var share = new Share();

           for (int i = 0; i < tickers.Length; i++)
           {
               var tmp = tickers[i];


               share.Name = names[i];
               share.Ticker = tmp;
               share.Market = cap[i];
               share.Country = uow.CountryRepository.GetSingle(x => x.Name.Equals("USA"));
               uow.ShareRepository.Add(share);
               uow.Save();
           }

       }


   }
}
