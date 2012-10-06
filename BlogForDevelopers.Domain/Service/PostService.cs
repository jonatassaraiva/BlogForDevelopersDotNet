using BlogForDevelopers.Domain.Entities;
using BlogForDevelopers.Domain.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogForDevelopers.Domain.Service
{
	public class PostService
	{
		private IRepository<Post> postRepository { get; set; }

		public PostService(IRepository<Post> postRepository)
		{
			this.postRepository = postRepository;
		}

		public void Create(Post post)
		{
			if (post.Published)
				this.Published(post);

			this.postRepository.Insert(post);
		}

		public void Update(Post post)
		{
			Post currentPost = this.postRepository.Get(post.Id);

			List<Comment> comments = currentPost.Comments.ToList();

			if (comments.Count > 0)
				comments.AddRange(post.Comments.ToList());

			post.Comments = comments;

			if (post.Published && !currentPost.Published)
				this.Published(post);
			else
				post.DatePublished = currentPost.DatePublished;

			post.DateCreated = currentPost.DateCreated;

			this.postRepository.Update(post);
		}

		public void Remove(Guid id)
		{
			this.postRepository.Remove(id);
		}


		public Post Get(Guid id)
		{
			return this.postRepository.Get(id);
		}

		public Post GetPublished(DateTime datePublished, string titleUrl)
		{
			Post post = (from p in this.postRepository.Data
						 where p.Published
								 && p.DateCreated.ToString("dd/MM/yyyy") == datePublished.ToString("dd/MM/yyyy")
								 && p.TitleUrl == titleUrl
						 select p).SingleOrDefault();

			return post;
		}

		public IEnumerable<Post> GetAllPublished()
		{
			return this.postRepository.Data
							.Where(p => p.Published)
							.OrderByDescending(p => p.DatePublished)
							.ToList();

		}

		public IEnumerable<Post> GetAllByTag(Tag tag)
		{
			IList<Post> posts = new List<Post>();

			foreach (var post in this.GetAllPublished())
			{
				Tag contensTag = post.Tags
										.Where(t => t.Name.ToLower() == tag.Name.ToLower())
										.SingleOrDefault();
				if (contensTag != null)
					posts.Add(post);
			}

			return posts;
		}

		private void Published(Post post)
		{
			post.DatePublished = DateTime.Now;
		}

		public IEnumerable<Post> GetAll()
		{
			return this.postRepository.Data.ToList();
		}

		public IEnumerable<Post> GetLastTop(int length)
		{
			return
					this.postRepository.Data
							.Where(p => p.Published)
							.OrderByDescending(p => p.DatePublished)
							.Take(length).ToList();
		}
	}
}
