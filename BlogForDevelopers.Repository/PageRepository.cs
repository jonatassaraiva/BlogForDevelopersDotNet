using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogForDevelopers.Repository
{
	public class PageRepository : BaseRepository<Page>, IRepository<Page>
	{
		public PageRepository(string serverMapPath) : base (serverMapPath)
		{ }

		public void Insert(Page entity)
		{
			entity.Id = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;

			XElement xPage = new XElement(base.FileNameIndex);
			this.BuildAttribute(entity, xPage);

			base.DocumentIndex.Root.Add(xPage);
			base.SaveChangeIndex();

			this.BuildElement(entity, xPage);

			base.SavaChangeDocument(entity, xPage);
		}

		public void Update(Page entity)
		{
			XElement xPage = new XElement(base.FileNameIndex);
			this.BuildAttribute(entity, xPage);

			this.BuildElement(entity, xPage);

			this.SavaChangeDocument(entity, xPage);
		}

		public Page Get(Guid id)
		{
			return this.GetPage(id, base.GetElement(id));
		}
		
		public IQueryable<Page> Data
		{
			get { return this.LoadData(); }
		}

		private IQueryable<Page> LoadData()
		{
			IList<Page> pages = new List<Page>();

			foreach (var elemente in base.DocumentIndex.Root.Elements(base.FileNameIndex))
			{
				Guid idPage = Guid.Parse(elemente.Attribute("Id").Value);
				XDocument document = base.LoadFile(base.FormatNameFile(idPage));

				if (document.Root.HasElements)
				{
					XElement xPage = document.Root.Element(base.FileNameIndex);

					Page page = this.GetPage(idPage, xPage);

					pages.Add(page);
				}
			}

			return pages.AsQueryable();
		}

		private Page GetPage(Guid idPage, XElement xPage)
		{
			Page page = new Page();
			page.Id = idPage;
			page.Published = Boolean.Parse(xPage.Attribute("Published").Value);
			page.DateCreated = DateTime.Parse(xPage.Attribute("DateCreated").Value);

			page.Content = xPage.Element("Content").Value;
			page.Title = xPage.Element("Title").Value;
			page.Uri = xPage.Element("Uri").Value;

			return page;
		}

		private void BuildElement(Page entity, XElement xPage)
		{
			xPage.Add(
				new XElement("Title", entity.Title),
				new XElement("Uri", entity.Uri),
				new XElement("Content", entity.Content)
			);
		}

		private void BuildAttribute(Page entity, XElement xPage)
		{
			xPage.Add(
				new XAttribute("Id", entity.Id),
				new XAttribute("Published", entity.Published),
				new XAttribute("DateCreated", entity.DateCreated)
			);
		}

	}
}
