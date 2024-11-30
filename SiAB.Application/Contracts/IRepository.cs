using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IRepository<T> where T : EntityMetadata
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task Update(T entity);
		Task DeleteById(int id);
		Task<TResult> FindWhereAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
		Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
		Task<IEnumerable<TResult>> GetListPaginateAsync<TResult>(Expression<Func<T, bool>> predicate, Expression <Func<T, TResult>> selector, int page = 1, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
		Task CommitChangeAsync();
		Task<bool> ConfirmExistsAsync(Expression<Func<T, bool>> predicate);
	}
}
