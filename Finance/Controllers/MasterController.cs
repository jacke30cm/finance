using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finance.Controllers
{
    public class MasterController : BaseController
    {

        public ActionResult Menu()
        {
            return PartialView("~/Views/Master/Menu.cshtml"); 
        }

        public ActionResult ContestControl(string name)
        {
            if (name.Equals("CreateCompetition"))
            {
                return PartialView("~/Views/Master/Control-Create-Competition.cshtml");
            }

            return PartialView("~/Views/Master/Control-Create-Competition.cshtml");
        }

        public ActionResult ShareControl(string name)
        {
            return PartialView("~/Views/Master/Control-Share-Action.cshtml");
        }
    }
}