using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace BlogForDevelopers.Domain.Service
{
	public class TagService
	{
		private IRepository<Post> postRepository { get; set; }

		public TagService(IRepository<Post> postRepository)
		{
			this.postRepository = postRepository;
		}

		public IEnumerable<Tag> GetAll()
		{
			var tagsInPosts = this.TagsInPosts();

			var tags = this.JoiningTags(tagsInPosts);

			return this.TagsDistinct(tags);
		}

		public IEnumerable<Tag> GetAllPublished()
		{
			var tagsInPostPublished = this.TagsInPostsPublished();

			var tags = this.JoiningTags(tagsInPostPublished);

			return this.TagsDistinct(tags);
		}

		public IDictionary<string, int> GetTagsNameCount()
		{
			var tagsInPostsPublished = this.TagsInPostsPublished();

			var tags = this.JoiningTags(tagsInPostsPublished);

			var tagDistictTotal = from tag in tags
								  group tag by tag.Name into g
								  select new
								  {
									  TagName = g.Key,
									  Count = g.Count()
								  };

			IDictionary<string, int> tagsNameCount = new Dictionary<string, int>();

			foreach (var item in tagDistictTotal)
				tagsNameCount.Add(item.TagName, item.Count);

			return tagsNameCount;
		}

		private IQueryable<IEnumerable<Tag>> TagsInPostsPublished()
		{
			return from p in this.postRepository.Data
				   where p.Published
				   select p.Tags;
		}

		private IQueryable<IEnumerable<Tag>> TagsInPosts()
		{
			return from p in this.postRepository.Data
				   select p.Tags;
		}

		private IEnumerable<Tag> JoiningTags(IQueryable<IEnumerable<Tag>> listsTagsInPostsPublished)
		{
			List<Tag> joiningTags = new List<Tag>();

			foreach (var rangeTags in listsTagsInPostsPublished)
				joiningTags.AddRange(rangeTags);

			return joiningTags.OrderBy(t=>t.Name);
		}

		private IList<Tag> TagsDistinct(IEnumerable<Tag> tags)
		{
			return (from tagName in
						(from c in tags select c.Name).Distinct()
					select new Tag()
					{
						Name = tagName
					}).ToList();
		}
	}
}
