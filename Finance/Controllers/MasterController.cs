using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Finance.Controllers
{
    public class MasterController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Menu()
        {
            var model = GetService.GetUser(User.Identity.GetUserId()); 
            return PartialView("~/Views/Master/Menu.cshtml", model); 
        }

        [Authorize]
        public ActionResult ContestControl(string name)
        {
            if (name.Equals("CreateCompetition"))
            {
                return PartialView("~/Views/Master/Control-Create-Competition.cshtml");
            }

            return PartialView("~/Views/Master/Control-Create-Competition.cshtml");
        }

        [Authorize]
        public ActionResult ShareControl(string name)
        {
            return PartialView("~/Views/Master/Control-Share-Action.cshtml");
        }
    }
}