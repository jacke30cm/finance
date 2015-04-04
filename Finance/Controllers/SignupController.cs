using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;


namespace Finance.Controllers
{
    [AllowAnonymous]
    public class SignupController : BaseController
    {
        
        public ActionResult Index(string returnUrl)
        {
            LocationHelper.Location = "Start";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}