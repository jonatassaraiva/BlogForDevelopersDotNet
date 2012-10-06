using BlogForDevelopers.Domain.ObjectValue;
using BlogForDevelopers.Domain.Service;
using BlogForDevelopers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.App_Helpers
{
	public static class TagHelper
	{
		private static TagService tagService;

		static TagHelper()
		{
			TagHelper.tagService = (TagService)DependencyResolver.Current.GetService(typeof(TagService)); 
		}

		public static string GetTagsPublishedSeparatedByCommas()
		{
			IList<string> tagsNames = TagHelper.GetTagsPublishedInCache()
				.Select(t=>t.Name)
				.ToList();

			return TagHelper.GetContent(tagsNames);
		}

		public static IEnumerable<Tag> GetTagsPublished()
		{
			return TagHelper.GetTagsPublishedInCache();
		}

		public static string GetContent(IEnumerable<Tag> tags)
		{
			IList<string> content = tags.Select(t => t.Name).ToList();

			return TagHelper.GetContent(content);
		}

		private static IEnumerable<Tag> GetTagsPublishedInCache()
		{
			IEnumerable<Tag> tags = (IEnumerable<Tag>)HttpRuntime.Cache.Get("TagsPublished");

			if (tags == null)
			{
				tags = TagHelper.tagService.GetAllPublished();

				HttpRuntime.Cache.Insert("Tags", tags, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
			}

			return tags;
		}

		private static string GetContent(IList<string> content)
		{
			string result = string.Empty;

			if (content.Count > 0)
			{

				for (int i = 0; i < content.Count; i++)
					result += string.Concat(content[i], ", ");

				result = result.TrimEnd();
				result = result.Remove((result.Length - 1));
			}

			return result;
		}
	}
}