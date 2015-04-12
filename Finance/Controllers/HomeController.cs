using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Data.Entities;
using Services;


namespace Finance.Controllers
{
    public class HomeController : BaseController
    {

       
        [Authorize]
        public ActionResult Index()
        {
            LocationHelper.Location = "Home"; 
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {

            var serv = new StockHandler();
            //serv.PopulateStockQuotes();
            //serv.UpdateDatbase();

            LocationHelper.Location = "About"; 
            return View();
        }

        public void GetNewStocks()
        {
            var serv = new StockHandler();
            serv.RequestStockQuotes();

        }



        
       
    }
}