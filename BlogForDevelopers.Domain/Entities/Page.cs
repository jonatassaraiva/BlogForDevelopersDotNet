using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Domain.Entities
{
	public class Page : Entity
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public string Uri { get; set; }

		public DateTime DateCreated { get; set; }

		public bool Published { get; set; }
	}
}
