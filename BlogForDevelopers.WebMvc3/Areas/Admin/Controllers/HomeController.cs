using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Areas.Admin.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
		[HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("index", "post");
        }

    }
}
