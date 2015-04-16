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
using Microsoft.AspNet.Identity;
using Services;


namespace Finance.Controllers
{
    public class HomeController : BaseController
    {

       
        [Authorize]
        public ActionResult Index()
        {
            var ser = new Get();
            

            LocationHelper.Location = "Home";
            var contestlist = ser.GetContestByUser(User.Identity.GetUserId());
            return View(contestlist);
        }

        [AllowAnonymous]
        public ActionResult About()
        {

            var serv = new StockHandler();
            //serv.PopulateStockQuotes();
            //serv.UpdateDatbase();
            //serv.RequestStockQuotes();
            //serv.UpdateDatbase();
            //serv.AddUSAStock();



            LocationHelper.Location = "About"; 
            return View();
        }

        public void GetNewStocks()
        {
            var serv = new StockHandler();
            serv.RequestStockQuotes();

        }

        public void SignUpContest()
        {

            var ser = new Post();
            ser.SignUpForContest(User.Identity.GetUserId(), 2);

        }

        public void GetContestByUser()
        {
            var ser = new Get();
            ser.GetContestByUser(User.Identity.GetUserId());
        }





    }
}