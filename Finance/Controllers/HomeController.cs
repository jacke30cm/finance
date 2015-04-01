using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Data.Entities;
using Services;

namespace Finance.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            LocationHelper.Location = "Home"; 
            return View();
        }

        public ActionResult About()
        {

            SplitCSV();
            LocationHelper.Location = "About"; 
            return View();
        }


        public void AddStockToDatabase(Share share)
        {

            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDb)\v11.0;AttachDbFilename=C:\Finance\finance\Finance\App_Data\Finance.mdf;Initial Catalog=Finance;Integrated Security=True");


            int a = 1;
            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO Shares(Name,Ticker,Market,Description) VALUES ('" + share.Name + "', '" + share.Ticker + "', '" + share.Market + "', '" + share.Description + "')", cn);

            cn.Open();
            cmd1.ExecuteNonQuery();
            cn.Close();


        }

        public static string GetCSV(string url)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        public void SplitCSV()
        {

            string[] lines = System.IO.File.ReadAllLines(@"C:\StockTickersSverige.txt");

            string builder = "";

            foreach (var line in lines)
            {

                builder += line + "+";

            }




            int counter = 0;

            string fileList = GetCSV("http://finance.yahoo.com/d/quotes.csv?s=" + builder + "&f=sn");
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
                        Share.Ticker = tempStr[i];
                    }
                    else if (counter == 1)
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
                    
                       
                    





                }
                if (counter == 1)
                {

                    Share.Market = "LargeCap";
                    Share.Description = "Cool aktie som går att köpa dyrt";
                    Share.Country = new Country() { Id = 1 };
                    AddStockToDatabase(Share);

                    counter = 0;
                    Share = new Share();
                }
                else
                {
                    counter++;
                }

            }


        }
       
    }
}