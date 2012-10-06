using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using BlogForDevelopers.Domain.Service;
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
	public class PostRepository : BaseRepository<Post>, IRepository<Post>
	{
		private string serverMapPath;

		public PostRepository(string serverMapPath)
			: base(serverMapPath)
		{
			this.serverMapPath = serverMapPath;
		}

		public void Insert(Post entity)
		{
			entity.Id = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;

			this.SaveIndex(entity);

			this.SavePost(entity);
		}

		public void Update(Post entity)
		{
			this.SavePost(entity);
		}

		public Post Get(Guid id)
		{
			return GetPost(id, base.GetElement(id));
		}

		public IQueryable<Post> Data
		{
			get { return LoadData(); }
		}

		private void SaveIndex(Post entity)
		{
			XElement xPostIndex = new XElement("Post");
			xPostIndex.Add(
				new XAttribute("Id", entity.Id));

			base.DocumentIndex.Root.Add(xPostIndex);
			base.SaveChangeIndex();
		}

		private void SavePost(Post entity)
		{
			XElement xPost = new XElement("Post");
			this.BuildAttibutes(entity, xPost);

			this.BuildElements(entity, xPost);

			this.SavaChangeDocument(entity, xPost);
		}

		private void BuildAttibutes(Post post, XElement xPost)
		{
			xPost.Add(
				new XAttribute("Id", post.Id),
				new XAttribute("Published", post.Published),
				new XAttribute("EnableComment", post.EnableComment),
				new XAttribute("DatePublished", post.DatePublished),
				new XAttribute("DateCreated", post.DateCreated)
			);
		}

		private void BuildElements(Post post, XElement xPost)
		{
			xPost.Add(
				new XElement("Title", post.Title),
				new XElement("TitleUrl", post.TitleUrl),
				new XElement("Content", post.Content)
			);

			XElement xTags = this.BuildTags(post);
			xPost.Add(xTags);

			XElement xComments = this.BuildComments(post);
			xPost.Add(xComments);
		}
		
		private XElement BuildComments(Post post)
		{
			XElement xComments = new XElement("Comments");
			foreach (var comment in post.Comments)
			{
				XElement xComment = new XElement("Comment");
				xComment.Add(
					new XAttribute("Id", comment.Id),
					new XAttribute("DateCreated", comment.DateCreated),
					new XElement("Name", comment.Name),
					new XElement("Email", comment.Email),
					new XElement("Content", comment.Content)
				);

				xComments.Add(xComment);
			}
			return xComments;
		}

		private XElement BuildTags(Post post)
		{
			XElement xTags = new XElement("Tags");
			foreach (var tag in post.Tags)
			{
				xTags.Add(
					new XElement("Tag", tag.Name)
				);
			}
			return xTags;
		}

		private Post GetPost(Guid idPost, XElement xPost)
		{
			if (xPost == null)
				return null;
			else
			{
				Post post = new Post();
				post.Id = idPost;
				post.Published = Boolean.Parse(xPost.Attribute("Published").Value);
				post.DateCreated = DateTime.Parse(xPost.Attribute("DateCreated").Value);
				post.DatePublished = DateTime.Parse(xPost.Attribute("DatePublished").Value);
				post.EnableComment = Boolean.Parse(xPost.Attribute("EnableComment").Value);

				post.Title = xPost.Element("Title").Value;
				post.TitleUrl = xPost.Element("TitleUrl").Value;
				post.Content = xPost.Element("Content").Value;

				post.Tags = this.GetTags(xPost);
				post.Comments = this.GetComments(xPost);
				return post;
			}
		}

		private IQueryable<Post> LoadData()
		{
			IList<Post> posts = new List<Post>();

			foreach (var elemente in base.DocumentIndex.Root.Elements("Post"))
			{
				Guid idPost = Guid.Parse(elemente.Attribute("Id").Value);

				XDocument document = base.LoadFile(this.FormatNameFile(idPost));

				if (document.Root.HasElements)
				{
					XElement xPost = document.Root.Element("Post");

					Post post = GetPost(idPost, xPost);

					posts.Add(post);
				}
			}

			return posts.AsQueryable();
		}

		private IEnumerable<Comment> GetComments(XElement xPost)
		{
			IList<Comment> comments = new List<Comment>();

			foreach (var item in xPost.Element("Comments").Elements("Comment"))
			{
				Comment comment = new Comment();

				comment.Id = Guid.Parse(item.Attribute("Id").Value);
				comment.DateCreated = DateTime.Parse(item.Attribute("DateCreated").Value);

				comment.Name = item.Element("Name").Value;
				comment.Email = item.Element("Email").Value;
				comment.Content = item.Element("Content").Value;

				comments.Add(comment);
			}

			return comments;
		}

		private IEnumerable<Tag> GetTags(XElement xPost)
		{
			IList<Tag> tags = new List<Tag>();

			foreach (var item in xPost.Element("Tags").Elements("Tag"))
			{
				tags.Add(new Tag()
				{
					Name = HttpUtility.HtmlDecode(item.Value)
				});
			}

			return tags;
		}
	}
}
