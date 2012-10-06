using BlogForDevelopers.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Controllers
{
	public class PostController : Controller
	{
		private PostService postService;

		public PostController(PostService postService)
		{
			this.postService = postService;
		}

		[HttpGet]
		public ActionResult Index(int day, int month, int year, string title)
		{
			DateTime datePublished = new DateTime(year, month, day);

			return View(this.postService.GetPublished(datePublished, title));
		}

		[HttpGet]
		public ActionResult Posts()
		{
			return View(this.postService.GetAllPublished());
		}

		[HttpGet]
		[ChildActionOnly]
		public PartialViewResult LastPost()
		{
			return PartialView("_LastPost", this.postService.GetLastTop(8));
		}
	}
}
