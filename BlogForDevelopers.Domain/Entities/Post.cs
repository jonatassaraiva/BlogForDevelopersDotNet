using BlogForDevelopers.Domain.ObjectValue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Domain.Entities
{
	public class Post : Entity
	{
		public Post()
		{
			this.Tags = new List<Tag>();
		}

        public string Title { get; set; }
        
        public string TitleUrl { get; set; }

		public string Content { get; set; }

		[DisplayName("Date of publication")]
		public DateTime DatePublished { get; set; }

		[DisplayName("Enable comment")]
		public bool EnableComment { get; set; }

		public bool Published { get; set; }

		[DisplayName("Date of creation")]
		public DateTime DateCreated { get; set; }

		public IEnumerable<Tag> Tags { get; set; }

		public IEnumerable<Comment> Comments { get; set; }
	}
}
