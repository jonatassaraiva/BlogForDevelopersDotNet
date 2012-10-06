using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogForDevelopers.WebMvc3.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Admin/Account/
		[HttpGet]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		public ActionResult Login(string username, string password, string returnUrl)
		{
			// Se usuário e senha estão corretos 
			if (FormsAuthentication.Authenticate(username, password))
			{
				// Autentica 
				FormsAuthentication.SetAuthCookie(username, false);

				return Redirect(returnUrl);
			}
			else
			{
				ViewBag.Mensage = "Usuário ou senha incorretos";
				return View();
			}
		}

		[HttpGet]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return Redirect("/");
		}

    }
}
