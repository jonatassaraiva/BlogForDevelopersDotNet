using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index(string name)
        {
			ViewBag.Name = name;
            return View();
        }

    }
}
