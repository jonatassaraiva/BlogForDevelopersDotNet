using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogForDevelopers.Domain.Entities
{
	public class Comment : Entity
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public string Content { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
