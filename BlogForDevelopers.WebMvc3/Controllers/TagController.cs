using BlogForDevelopers.Domain.ObjectValue;
using BlogForDevelopers.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogForDevelopers.WebMvc3.Controllers
{
    public class TagController : Controller
    {
		private TagService tagService;
		private PostService postService;

		public TagController(TagService tagService, PostService postService)
		{
			this.tagService = tagService;
			this.postService = postService;
		}
		
        // GET: /Tags/
        public ActionResult Index(string name)
        {
			Tag tag = new Tag() { 
				Name = name
			};

            return View(this.postService.GetAllByTag(tag));
        }

		[HttpGet]
		[ChildActionOnly]
		public PartialViewResult TagsCloud()
		{
			return PartialView("_TagsCloud", this.tagService.GetTagsNameCount());
		}
	
    }
}
