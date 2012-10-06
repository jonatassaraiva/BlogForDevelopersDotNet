using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using BlogForDevelopers.Domain.Service;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Areas.Admin.Controllers
{
	[Authorize]
    public class PostController : Controller
    {
		private TagService tagService;
		private PostService postService;

		public PostController(TagService tagService, PostService postService)
		{
			this.tagService = tagService;
			this.postService = postService;
		}


        /// <summary>
		/// GET: /Admin/Post/ 
        /// </summary>
        /// <returns>View: Index</returns>
        [HttpGet]
		public ActionResult Index()
        {
            return View(this.postService.GetAll().OrderByDescending(p=>p.DateCreated));
        }

		/// <summary>
		/// GET:  /Admin/Post/Create
		/// </summary>
		/// <returns>View: Form</returns>
		[HttpGet]
		public ActionResult Create()
		{
            ViewBag.Title = "Create";
            ViewBag.Tags = tagService.GetAll();

			Post post = new Post();

			return View("Form", post);
		}

		/// <summary>
		/// POST:  /Admin/Post/Create
		/// </summary>
		/// <param name="post">Objec Post</param>
		/// <param name="tags">Tags separated with ","</param>
		/// <returns>Redirect to action Edit</returns>
		[HttpPost, ValidateInput(false)]
		public ActionResult Create(Post post, string tags)
		{
            this.BuildPost(post, tags);

			this.postService.Create(post);

			return RedirectToAction("edit", new { id = post.Id });
		}

		/// <summary>
		/// GET:  /Admin/Post/Edit/{id}
		/// </summary>
		/// <param name="id">Identifier of Post</param>
		/// <returns>View Form</returns>
		[HttpGet]
		public ActionResult Edit(string id)
		{
            ViewBag.Title = "Edit";
			ViewBag.Tags = tagService.GetAll();

			Post post = this.postService.Get(Guid.Parse(id));

			return View("Form", post);
		}

		//
		// POST:  /Admin/Post/Edit
		[HttpPost, ValidateInput(false)]
		public ActionResult Edit(Post post, string tags)
		{
            this.BuildPost(post, tags);

			this.postService.Update(post);

			return RedirectToAction("edit", new { id = post.Id });
		}

        //
        // POST: /Admin/Post/Remove
        [HttpPost]
        public RedirectToRouteResult Remove(string id)
        {
            this.postService.Remove(Guid.Parse(id));
            return RedirectToAction("index");
        }

        private void BuildPost(Post post, string tags)
        {
            IList<Tag> tagsList = new List<Tag>();

            foreach (var tagName in tags.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(tagName))
                {
                    Tag tag = new Tag()
                    {
                        Name = tagName.Trim()
                    };

                    tagsList.Add(tag);
                }
            }

            post.Tags = tagsList;
            post.Comments = new List<Comment>();
        }
    }
}
