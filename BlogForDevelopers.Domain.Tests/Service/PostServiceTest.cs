using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using BlogForDevelopers.Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogForDevelopers.Domain.Tests.Service
{
	[TestClass]
	public class PostServiceTest : BaseTest
	{
		[TestMethod]
		public void GetAllByTag_Test()
		{
			var moqRepositoryPost = new Mock<IRepository<Post>>();

			moqRepositoryPost.Setup(p => p.Data).Returns(base.BuildPosts().AsQueryable());

			PostService postService = new PostService(moqRepositoryPost.Object);

			Tag tag = new Tag();
			tag.Name = "ASP.NET";

			Assert.IsTrue( postService.GetAllByTag(tag).Count() == 2 );
		}
		
	}
}
