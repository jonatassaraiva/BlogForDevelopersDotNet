using BlogForDevelopers.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogForDevelopers.Repository.Tests
{
	[TestClass]
	public class ContentPlaceRepositotyTest : BaseTest
	{
		[TestMethod]
		public void InsetContentPlace_Test()
		{
			ContentPlace contentPlace = CreateContentPlace();

			ContentPlaceRepository repository = new ContentPlaceRepository(BaseTest.ServerMapPath);
			repository.Insert(contentPlace);

			Assert.AreNotEqual(Guid.Empty, contentPlace.Id, "The object ContentPlace was not created in the repository");

			string filePathPage = string.Concat(BaseTest.StorageXmlPath, string.Format("ContentPlace_{0}.xml", contentPlace.Id));

			Assert.IsTrue(System.IO.File.Exists(filePathPage), "The file xml page was not created");

			XDocument xDocument = XDocument.Load(filePathPage);

			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Attribute("Id") != null, "There is no attribute Id");
			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Attribute("Published") != null, "There is no attribute Published");
			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Attribute("DateCreated") != null, "There is no attribute DateCreated");
			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Attribute("Type") != null, "There is no attribute Type");
			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Attribute("Code") != null, "There is no element Code");

			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Element("Title") != null, "There is no element Title");
			Assert.IsTrue(xDocument.Root.Element("ContentPlace").Element("Content") != null, "There is no element Content");
		}

		[TestMethod]
		public void EditContentPlace_Test()
		{
			ContentPlace contentPlace = this.CreateContentPlace();
			contentPlace.Id = Guid.Parse("ba7542bf-0a41-417b-a684-e0843151dfb4");

			contentPlace.DateCreated = DateTime.Now;

			ContentPlaceRepository repositoy = new ContentPlaceRepository(BaseTest.ServerMapPathFacked);

			repositoy.Update(contentPlace);

			XDocument xContentPlaceUpdated = XDocument.Load(string.Concat(BaseTest.ServerMapPathFacked, "App_Data\\", "ContentPlace_", contentPlace.Id, ".xml"));

			Assert.IsNotNull(xContentPlaceUpdated, "Document does not exist");

			DateTime dateUpdateFomDocument = DateTime.Parse(xContentPlaceUpdated.Root.Element("ContentPlace").Attribute("DateCreated").Value);

			Assert.IsTrue(dateUpdateFomDocument.Equals(contentPlace.DateCreated), "DateCreated does not upadated");
		}

		[TestMethod]
		public void DataContentPlace_Test()
		{
			ContentPlaceRepository repository = new ContentPlaceRepository(BaseTest.ServerMapPathFacked);

			var result = repository.Data.ToList();

			Assert.IsTrue(result.Count == 3, "There are no files xml");
		}

		[TestMethod]
		public void GetContentPlace_Test()
		{
			ContentPlaceRepository repository = new ContentPlaceRepository(BaseTest.ServerMapPathFacked);

			ContentPlace contentPlace = repository.Get(Guid.Parse("ba7542bf-0a41-417b-a684-e0843151dfb4"));
			
			Assert.IsNotNull(contentPlace);
		}

		private ContentPlace CreateContentPlace()
		{
			ContentPlace contentPlace = new ContentPlace();
			contentPlace.Code = "ContentTest";
			contentPlace.Content = "This is the <b> content of content place </b>";
			contentPlace.Title = "This is a title";
			contentPlace.Type = "Agenda";

			return contentPlace;
		}
	}
}
