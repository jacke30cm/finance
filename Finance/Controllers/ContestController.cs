using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Finance.Controllers
{
    public class ContestController : Controller
    {
        
        public ActionResult Index()
        {
            LocationHelper.Location = "Contest"; 
            return View();
        }
    }
}