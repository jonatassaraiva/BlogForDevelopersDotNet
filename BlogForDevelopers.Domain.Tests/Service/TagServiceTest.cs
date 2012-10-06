using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Domain.Tests.Service
{
	[TestClass]
	public class TagServiceTest : BaseTest
	{
		[TestMethod]
		public void GetAllTagsPublished_Test()
		{
			var moqRepositoryPost = new Mock<IRepository<Post>>();

			moqRepositoryPost.Setup(p => p.Data).Returns(base.BuildPosts().AsQueryable());

			TagService service = new TagService(moqRepositoryPost.Object);

			var result = service.GetAllPublished();

			Assert.IsTrue(result.Count() == 5, "The number of tags distinctis");
		}

		[TestMethod]
		public void GetTagsNameCount_Test()
		{
			var moqRepositoryPost = new Mock<IRepository<Post>>();

			moqRepositoryPost.Setup(p => p.Data).Returns(base.BuildPosts().AsQueryable());

			TagService service = new TagService(moqRepositoryPost.Object);

			var result = service.GetTagsNameCount();

			Assert.IsTrue(result["ASP.NET"] == 2);
			Assert.IsTrue(result["ASP.NET MVC"] == 2);
			Assert.IsTrue(result["ASP.NET MVC 3"] == 2);
			Assert.IsTrue(result["C#"] == 1);
			Assert.IsTrue(result["Unit Test"] == 3);
		}
	}
}
