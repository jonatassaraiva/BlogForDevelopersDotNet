using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogForDevelopers.Domain.Tests.Service
{
	public class BaseTest
	{
		public IEnumerable<Post> BuildPosts()
		{
			List<Post> posts = new List<Post>();

			posts.Add(
				new Post()
				{
					Published = true,
					Tags = new List<Tag>() { 
						new Tag(){Name="Unit Test"},
						new Tag(){Name="ASP.NET"},
						new Tag(){Name="ASP.NET MVC"},
						new Tag(){Name="ASP.NET MVC 3"},
					}
				}
			);

			posts.Add(
				new Post()
				{
					Published = true,
					Tags = new List<Tag>() { 
						new Tag(){Name="Unit Test"},
						new Tag(){Name="ASP.NET"},
						new Tag(){Name="ASP.NET MVC"},
						new Tag(){Name="ASP.NET MVC 3"},
					}
				}
			);

			posts.Add(
				new Post()
				{
					Published = true,
					Tags = new List<Tag>() { 
						new Tag(){Name="Unit Test"},
						new Tag(){Name="C#"}
					}
				}
			);

			return posts;
		}
	}
}
