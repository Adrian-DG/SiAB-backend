﻿using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Models;

namespace SiAB.Application.Contracts
{
	public interface IRepository<T> where T : EntityMetadata
	{
		Task<IEnumerable<T>> GetAllAsync(bool ignoreFilter = false);
		Task<T> GetByIdAsync(int id);
		Task<int> AddAsync(T entity);
		Task Update(T entity);
		Task DeleteById(int id);
		Task<TResult> FindWhereAsync<TResult>(Expression<Func<T, bool>>? predicate, Expression<Func<T, TResult>> selector, params Expression<Func<T, object>>[]? includes);
		Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>>? predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null, bool ignoreFilter = false, params Expression<Func<T, object>>[]? includes);
		Task<PagedData<TResult>> GetListPaginateAsync<TResult>(Expression<Func<T, bool>>? predicate, Expression <Func<T, TResult>> selector,  Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null, int page = 1, int pageSize = 10, params Expression<Func<T, object>>[]? includes) where TResult : class;
		Task CommitChangeAsync();
		Task<bool> ConfirmExistsAsync(Expression<Func<T, bool>>? predicate);
	}
}
