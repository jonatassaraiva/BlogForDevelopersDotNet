using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogForDevelopers.WebMvc3
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


			routes.MapRoute(
				name: "Posts",
				url: "posts",
				defaults: new { controller = "Post", action = "Posts" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "About",
				url: "about",
				defaults: new { controller = "About", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "Contact",
				url: "contact",
				defaults: new { controller = "Contact", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "Home",
				url: "home",
				defaults: new { controller = "Home", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "Tags",
				url: "tag/{name}",
				defaults: new { controller = "Tag", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "Pages",
				url: "{name}",
				defaults: new { controller = "Page", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}",
				defaults: new { controller = "Home", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);
			
			
			
			routes.MapRoute(
				name: "Post",
				url: "post/{day}/{month}/{year}/{title}",
				defaults: new { controller = "Post", action = "Index" },
				namespaces: new string[] { "BlogForDevelopers.WebMvc3.Controllers" }
			);
		}
	}
}