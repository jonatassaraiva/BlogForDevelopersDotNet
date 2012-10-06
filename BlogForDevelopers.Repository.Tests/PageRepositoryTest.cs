using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogForDevelopers.Domain.Entities;
using System.Configuration;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace BlogForDevelopers.Repository.Tests
{
	[TestClass]
	public class PageRepositoryTest : BaseTest
	{
		[TestMethod]
		public void InsertPage_Test()
		{
			Page page = BuildObjectPage();

			PageRepository repository = new PageRepository(BaseTest.ServerMapPath);
			repository.Insert(page);

			Assert.AreNotEqual(Guid.Empty, page.Id, "The object page was not created in the repository");

			string filePathPage = string.Concat(BaseTest.StorageXmlPath, string.Format("Page_{0}.xml", page.Id));

			Assert.IsTrue(System.IO.File.Exists(filePathPage), "The file xml page was not created");

			XDocument xDocument = XDocument.Load(filePathPage);

			Assert.IsTrue(xDocument.Root.Element("Page").Attribute("Id") != null, "There is no attribute Id");
			Assert.IsTrue(xDocument.Root.Element("Page").Attribute("Published") != null, "There is no attribute Published");
			Assert.IsTrue(xDocument.Root.Element("Page").Attribute("DateCreated") != null, "There is no attribute DateCreated");

			Assert.IsTrue(xDocument.Root.Element("Page").Element("Title") != null, "There is no element Title");
			Assert.IsTrue(xDocument.Root.Element("Page").Element("Uri") != null, "There is no element Uri");
			Assert.IsTrue(xDocument.Root.Element("Page").Element("Content") != null, "There is no element Content");
		}

		[TestMethod]
		public void UpdatePage_Test()
		{
			Page page = this.BuildObjectPage();
			page.Id = Guid.Parse("b5e0c6ec-40d7-46c8-91a2-a0fe3328f6e3");
			page.DateCreated = DateTime.Now;

			PageRepository repository = new PageRepository(BaseTest.ServerMapPathFacked);

			repository.Update(page);

			XDocument xDocument = XDocument.Load(string.Concat(BaseTest.StorageXmlPathFacked, "Page_", page.Id, ".xml"));

			DateTime dateUpdateFomDocument = DateTime.Parse(xDocument.Root.Element("Page").Attribute("DateCreated").Value);

			Assert.IsTrue(dateUpdateFomDocument == page.DateCreated, "DatePublished does not upadated");
		}

		[TestMethod]
		public void RemovePage_Test()
		{
			Page page1 = this.BuildObjectPage();
			Page page2 = this.BuildObjectPage();

			PageRepository repository = new PageRepository(BaseTest.ServerMapPath);
			repository.Insert(page1);
			repository.Insert(page2);

			repository.Remove(page1.Id);

			string file = string.Concat(BaseTest.ServerMapPath, "Page_", page1.Id, ".xml");

			Assert.IsFalse(File.Exists(file), "The fille exist: " + file);

			XDocument documnetRepository = XDocument.Load(BaseTest.StorageXmlPath + "Page.xml");

			XElement pageInReposioty = documnetRepository.Root
				.Elements("Page")
				.Where(p => p.Attribute("Id").Value == page1.Id.ToString())
				.SingleOrDefault();

			Assert.IsNull(pageInReposioty, "The element Page does not removed");
		}

		[TestMethod]
		public void DataPage_Test()
		{
			PageRepository repository = new PageRepository(string.Concat(BaseTest.ServerMapPathFacked));

			var result = repository.Data.ToList();

			Assert.IsTrue(result.Count == 3, "There are no files xml");
		}

		[TestMethod]
		public void GetPage_Test()
		{
			PageRepository repository = new PageRepository(BaseTest.ServerMapPathFacked);

			Page page = repository.Get(Guid.Parse("c5dc1586-7d9b-4935-a79c-b89c2586c678"));

			Assert.IsNotNull(page);
		}





		private Page BuildObjectPage()
		{
			Page page = new Page();
			page.Content = "This is content of <b>page</b>.";
			page.DateCreated = DateTime.Now;
			page.Published = true;
			page.Title = "This is a title of page.";
			page.Uri = "About/JonatasSaraiva";
			return page;
		}
	}
}
