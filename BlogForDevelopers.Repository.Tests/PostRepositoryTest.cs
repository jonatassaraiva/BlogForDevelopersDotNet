using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogForDevelopers.Repository.Tests
{
	[TestClass]
	public class PostRepositoryTest : BaseTest
	{
		[TestMethod]
		public void InsertPost_Test()
		{
			PostRepository repository = new PostRepository(BaseTest.ServerMapPath);

			Post post = CreatePost();

			repository.Insert(post);

			string filePathPostRepository = string.Concat(BaseTest.StorageXmlPath, "Post.xml");

			string filePathPostOne = string.Concat(BaseTest.StorageXmlPath, string.Format("Post_{0}.xml", post.Id));

			Assert.IsTrue(System.IO.File.Exists(filePathPostRepository), "The file not created");

			Assert.IsTrue(System.IO.File.Exists(filePathPostOne), "The file not created");

			XDocument xDocument = XDocument.Load(filePathPostOne);

			Assert.IsTrue(xDocument.Root.HasElements, "There is no node Post");

			Assert.IsTrue(xDocument.Root.Element("Post").Element("Tags") != null, "There is no node Tags");

			Assert.IsTrue(xDocument.Root.Element("Post").Element("Comments") != null, "There is no node Categories");
		}

		[TestMethod]
		public void UpdatePost_Test()
		{
			Post post = CreatePost();
			post.Id = Guid.Parse("6db3c056-496d-492e-8282-4c86d34d2e59");

			post.DatePublished = DateTime.Now;

			PostRepository repositoy = new PostRepository(BaseTest.ServerMapPathFacked);

			repositoy.Update(post);

			XDocument xPostUpdated = XDocument.Load(string.Concat(BaseTest.StorageXmlPathFacked, "Post_", post.Id, ".xml"));
			
			Assert.IsNotNull(xPostUpdated, "Document does not exist");

			DateTime dateUpdateFomDocument = DateTime.Parse(xPostUpdated.Root.Element("Post").Attribute("DatePublished").Value);
			Assert.IsTrue(dateUpdateFomDocument.Equals(post.DatePublished), "DatePublished does not upadated");
		}

		[TestMethod]
		public void RemovePost_Test()
		{
			Post post1 = CreatePost();
			Post post2 = CreatePost();

			PostRepository repository = new PostRepository(BaseTest.ServerMapPath);

			repository.Insert(post1);
			repository.Insert(post2);

			repository.Remove(post1.Id);

			string file = string.Concat(BaseTest.StorageXmlPath, "Post_", post1.Id, ".xml");

			Assert.IsFalse(File.Exists(file), "The fille exist: " + file);

			XDocument documnetRepository = XDocument.Load(BaseTest.StorageXmlPath + "Post.xml");
			
			XElement postInReposioty = documnetRepository.Root
				.Elements("Post")
				.Where(p=>p.Attribute("Id").Value == post1.Id.ToString())
				.SingleOrDefault();

			Assert.IsNull(postInReposioty, "The element Post does not removed");
		}
		
		[TestMethod]
		public void DataPost_Test()
		{
			PostRepository repository = new PostRepository(BaseTest.ServerMapPathFacked);

			var result = repository.Data.ToList();

			Assert.IsTrue(result.Count > 0, "There are no files xml");

			Assert.IsFalse(result[0].Tags == null, "Tags is null");

			Assert.IsTrue(result[0].Tags.Count() == 3, "There are no tags");

			Assert.IsFalse(result[0].Comments == null, "Comments is null");

			Assert.IsTrue(result[0].Comments.Count() > 0, "There are no comments");
		}

		[TestMethod]
		public void GetPost_Test()
		{
			PostRepository repository = new PostRepository(BaseTest.ServerMapPathFacked);
			Post post = repository.Get(Guid.Parse("6db3c056-496d-492e-8282-4c86d34d2e59"));

			Assert.IsNotNull(post);
		}

		private Post CreatePost()
		{
			Post post = new Post();
			post.Id = Guid.NewGuid();
			post.Title = "Unit teste Insert - 1";
			post.Content
				= "Content post, post, postpostpostpostpost post <b/> <b> <a herf=\"www.jonatassariva.net\"> terra </a>";
			post.Published = true;

			post.DateCreated = DateTime.Now;
			post.DatePublished = post.DateCreated;
			post.EnableComment = true;

			IList<Tag> tags = new List<Tag>();
			tags.Add(new Tag() { Name = "Unit Test" });
			tags.Add(new Tag() { Name = "Blog" });
			tags.Add(new Tag() { Name = "Repository" });
			post.Tags = tags;

			IList<Comment> comments = new List<Comment>();
			comments.Add(new Comment()
			{
				Name = "One Unit Test",
				Content = "One Unit Test Unit Test",
				Email = "OneUnit@Test.com",
				Id = Guid.NewGuid(),
				DateCreated = DateTime.Now
			});
			comments.Add(new Comment()
			{
				Name = "Two Unit Test",
				Content = "Two Unit Test Unit Test",
				Email = "TwoUnit@Test.com",
				Id = Guid.NewGuid(),
				DateCreated = DateTime.Now
			});
			post.Comments = comments;

			return post;
		}
	}
}

