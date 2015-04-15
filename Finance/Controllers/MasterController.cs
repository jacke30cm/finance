using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Microsoft.AspNet.Identity;
using Services.ViewModels;

namespace Finance.Controllers
{
    public class MasterController : BaseController
    {
        public static string UploadedFileUrl; 


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



        [Authorize]
        public ActionResult ShareFilter()
        {
            return PartialView("~/Views/Master/Control-Share-Filter.cshtml"); 

        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateContest(CreateContestPostModel model)
        {
            // Create contest first, then return html-view containing the contest-object
            var contestId = PostService.CreateContest(model, UploadedFileUrl, User.Identity.GetUserId());
            var viewmodel = GetService.GetBasicContestData(contestId); 
            return PartialView("~/Views/Home/MyContests/SingleContest.cshtml", viewmodel);
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ContestImage()
        {
            // Try to save file 
            var file = Request.Files[0];
            var url = ImageService.UploadImage(file, "Contest");

            // If it was successful, the name(url) must be saved and attached to next following ajax-call to "CreateContest". 
            if (url != null)
            {
                UploadedFileUrl = url; 
            }

            return Json(url != null ? "Success" : "Failure", JsonRequestBehavior.AllowGet);
        }

        //HelperCalls

        [AllowAnonymous]
        [HttpGet]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult EmailAvailability(string email)
        {
            return Json(GetService.EmailAvailability(email).ToString(), JsonRequestBehavior.AllowGet);
        }



    }
}