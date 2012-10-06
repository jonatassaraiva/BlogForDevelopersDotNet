using BlogForDevelopers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogForDevelopers.Domain.Service
{
	public interface IRepository<T> where T : Entity
	{
		void Insert(T entity);

		void Update(T entity);

		T Get(Guid id);

		void Remove(Guid id);

		IQueryable<T> Data { get; }
	}
}
