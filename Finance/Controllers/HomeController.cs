﻿using System;
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

   
        
        public ActionResult Index()
        {
            LocationHelper.Location = "Home"; 
            return View();
        }

        public ActionResult About()
        {

            var serv = new StockHandler();
            serv.PopulateStockQuotes();

            LocationHelper.Location = "About"; 
            return View();
        }



        
       
    }
}