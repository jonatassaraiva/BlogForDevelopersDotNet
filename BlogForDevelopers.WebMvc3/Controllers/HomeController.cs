using BlogForDevelopers.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Controllers
{
    public class HomeController : Controller
    {
		//
        // GET: /Home/
		[HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
