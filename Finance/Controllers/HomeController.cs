using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            LocationHelper.Location = "About"; 
            return View();
        }
       
    }
}