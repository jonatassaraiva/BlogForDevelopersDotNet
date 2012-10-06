using BlogForDevelopers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace BlogForDevelopers.Repository
{
	public class BaseRepository<T> where T : Entity
	{
		protected XDocument DocumentIndex { get { return documentIndex; } }
		public string FileNameIndex { get; private set; }
		public string RepositoryPath { get; private set; }

		private XDocument documentIndex;
		private string fileRepository;
		private static string storageXml = ConfigurationManager.AppSettings["StorageXml"];

		public BaseRepository(string serverMapPath)
		{
			this.FileNameIndex = typeof(T).Name;

			this.RepositoryPath = string.Concat(serverMapPath, storageXml);

			this.fileRepository =
				String.Format("{0}{1}.xml", this.RepositoryPath, this.FileNameIndex);

			this.documentIndex = this.LoadFile(this.FileNameIndex);
		}

		protected XDocument LoadFile(string nameXml)
		{
			XDocument resultDocument = (XDocument)HttpRuntime.Cache.Get(nameXml);

			if (resultDocument == null)
			{
				var fileXml = string.Concat(this.RepositoryPath, nameXml, ".xml");

				if (System.IO.File.Exists(fileXml))
				{
					resultDocument = XDocument.Load(fileXml);

					this.PutInCache(nameXml, resultDocument, fileXml);
				}
				else
					resultDocument = new XDocument(new XElement("Root"));
			}

			return resultDocument;
		}

		private void PutInCache(string key, XDocument value, string fileDependency)
		{
			CacheDependency cacheDependency = new CacheDependency(fileDependency);

			HttpRuntime.Cache.Insert(key, value, cacheDependency);
		}

		protected void SaveChangeIndex()
		{
			this.documentIndex.Save(this.fileRepository);

			this.documentIndex = this.LoadFile(this.FileNameIndex);
		}

		protected void SavaChangeDocument(T entity, XElement xElement)
		{
			XDocument xDocumment = new XDocument(new XElement("Root"));

			xDocumment.Root.Add(xElement);

			xDocumment.Save(this.FormatFileNameWithoutPathAndExtension(entity.Id));
		}

		protected string FormatNameFile(Guid id)
		{
			return string.Concat(this.FileNameIndex, "_", id.ToString());
		}

		protected string FormatFileNameWithoutPathAndExtension(Guid id)
		{
			return string.Concat(this.RepositoryPath, this.FormatNameFile(id), ".xml");
		}

		protected XElement GetElement(Guid id)
		{
			XDocument document = this.LoadFile(this.FormatNameFile(id));

			XElement elementInRepository = document.Root
				.Elements(this.FileNameIndex)
				.Where(p => p.Attribute("Id").Value == id.ToString())
				.SingleOrDefault();

			return elementInRepository;
		}

		public void Remove(Guid id)
		{
			XElement elementPostInRepository = this.DocumentIndex.Root
				.Elements(this.FileNameIndex)
				.Where(p => p.Attribute("Id").Value == id.ToString())
				.SingleOrDefault();

			elementPostInRepository.Remove();

			this.SaveChangeIndex();

			string file = this.FormatFileNameWithoutPathAndExtension(id);

			if (File.Exists(file))
				File.Delete(file);
		}
	}
}
