using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.Service;
using BlogForDevelopers.Repository;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogForDevelopers.WebMvc3
{
	public class MvcApplication : NinjectHttpApplication
	{
		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel(new NinjectSettings { UseReflectionBasedInjection = true });
			kernel.Load(Assembly.GetExecutingAssembly());


			kernel.Bind<IRepository<Post>>().To<PostRepository>()
			.WithConstructorArgument("serverMapPath", HttpContext.Current.Server.MapPath("~/"));

			kernel.Bind<TagService>().To<TagService>();
			kernel.Bind<PostService>().To<PostService>();

			return kernel;
		}


		protected override void OnApplicationStarted()
		{
			base.OnApplicationStarted();

			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}