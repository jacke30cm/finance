using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Finance.Controllers
{
    public class BaseController : Controller
    {
        internal Get GetService;
        internal Post PostService;
        internal ImageService ImageService;

        public BaseController()
        {
            GetService = new Get();
            PostService = new Post();
            ImageService = new ImageService();
        }

    }
}