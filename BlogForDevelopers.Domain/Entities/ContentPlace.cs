using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Domain.Entities
{
	public class ContentPlace : Entity
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public string Code { get; set; }

		public string Type { get; set; }

		public bool Published { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
