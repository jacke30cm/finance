using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Finance.Controllers
{
    [Authorize]
    public class ContestController : BaseController
    {
        
        public ActionResult Index()
        {
            LocationHelper.Location = "Contest"; 
            return View();
        }


       // OVERVIEW 
        public ActionResult Overview()
        {
            return PartialView("~/Views/Contest/Overview.cshtml");
        }


        // PORTFOLIO
        public ActionResult Portfolio()
        {
            return PartialView("~/Views/Contest/Portfolio.cshtml");
        }


        // MARKET
        public ActionResult Market()
        {
            return PartialView("~/Views/Contest/Market.cshtml");
        }
        // MARKET -> MARKKET-SEARCH
        public ActionResult MarketSearch()
        {
            var model = GetService.GetBasicShareData(); 
            return PartialView("~/Views/Contest/Market/Market-Search.cshtml", model);
        }


        //CONTEST-INFO
        public ActionResult Details()
        {
            return PartialView("~/Views/Contest/Details.cshtml");
        }
    }
}