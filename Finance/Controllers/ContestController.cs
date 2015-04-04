using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Finance.Controllers
{
    [Authorize]
    public class ContestController : Controller
    {
        
        public ActionResult Index()
        {
            LocationHelper.Location = "Contest"; 
            return View();
        }


        // Sections
        public ActionResult Overview()
        {
            return PartialView("~/Views/Contest/Overview.cshtml");
        }

        public ActionResult Portfolio()
        {
            return PartialView("~/Views/Contest/Portfolio.cshtml");
        }

        public ActionResult Market()
        {
            return PartialView("~/Views/Contest/Market.cshtml");
        }

        public ActionResult Details()
        {
            return PartialView("~/Views/Contest/Details.cshtml");
        }
    }
}