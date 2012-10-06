using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogForDevelopers.Repository
{
	public class ContentPlaceRepository : BaseRepository<ContentPlace>, IRepository<ContentPlace>
	{
		public ContentPlaceRepository(string serverMapPath) : base(serverMapPath)
		{ }

		public void Insert(ContentPlace entity)
		{
			entity.Id = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;
			
			XElement xContentPlace = new XElement(base.FileNameIndex);

			this.BuildAttribute(entity, xContentPlace);

			base.DocumentIndex.Root.Add(xContentPlace);
			base.SaveChangeIndex();

			this.BuildElement(entity, xContentPlace);

			base.SavaChangeDocument(entity, xContentPlace);
		}

		public void Update(ContentPlace entity)
		{
			XElement xContentPlace = new XElement(base.FileNameIndex);

			this.BuildAttribute(entity, xContentPlace);
			this.BuildElement(entity, xContentPlace);

			base.SavaChangeDocument(entity, xContentPlace);
		}

		public ContentPlace Get(Guid id)
		{
			return this.GetContentPlace(id, base.GetElement(id));
		}

		public IQueryable<ContentPlace> Data
		{
			get { return this.LoadData(); }
		}

		private IQueryable<ContentPlace> LoadData()
		{
			IList<ContentPlace> contentPages = new List<ContentPlace>();

			foreach (var elemente in base.DocumentIndex.Root.Elements(base.FileNameIndex))
			{
				Guid id = Guid.Parse(elemente.Attribute("Id").Value);

				XDocument document = base.LoadFile(base.FormatNameFile(id));

				if (document.Root.HasElements)
				{
					XElement xContentPlace = document.Root.Element(base.FileNameIndex);

					ContentPlace contentPlace = GetContentPlace(id, xContentPlace);

					contentPages.Add(contentPlace);
				}
			}

			return contentPages.AsQueryable();
		}

		private ContentPlace GetContentPlace(Guid id, XElement xContentPlace)
		{
			if (xContentPlace == null)
				return null;
			else
			{
				ContentPlace contentPlace = new ContentPlace();
				contentPlace.Id = id;
				contentPlace.Published = Boolean.Parse(xContentPlace.Attribute("Published").Value);
				contentPlace.Code = xContentPlace.Attribute("Code").Value;
				contentPlace.DateCreated = DateTime.Parse(xContentPlace.Attribute("DateCreated").Value);
				contentPlace.Type = xContentPlace.Attribute("Type").Value;

				contentPlace.Content = xContentPlace.Element("Content").Value;
				contentPlace.Title = xContentPlace.Element("Title").Value;

				return contentPlace;
			}
		}

		private void BuildElement(ContentPlace entity, XElement xContentPlace)
		{
			xContentPlace.Add(
				new XElement("Content", entity.Content),
				new XElement("Title", entity.Title)
			);
		}

		private void BuildAttribute(ContentPlace entity, XElement xContentPlace)
		{
			xContentPlace.Add(
				new XAttribute("Id", entity.Id.ToString()),
				new XAttribute("Published", entity.Published),
				new XAttribute("Code", entity.Code),
				new XAttribute("DateCreated", entity.DateCreated),
				new XAttribute("Type", entity.Type)
			);
		}
	}
}
