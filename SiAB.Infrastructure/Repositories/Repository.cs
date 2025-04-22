using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Models;
using AutoMapper.Configuration.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SiAB.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : EntityMetadata
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _repository;
		public Repository(AppDbContext dbContext)
		{
			_context = dbContext;
			_repository = _context.Set<T>();
		}

		public async Task<int> AddAsync(T entity)
		{
			await _repository.AddAsync(entity);
			await CommitChangeAsync();
			return entity.Id;
		}

		public async Task CommitChangeAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ConfirmExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await _repository.IgnoreQueryFilters().AnyAsync(predicate);
		}

		public async Task DeleteById(int id)
		{
			var entity = await GetByIdAsync(id);

			if (entity is null) return;

			_repository.Remove(entity);

			await CommitChangeAsync();
		}

		public async Task<TResult> FindWhereAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, params Expression<Func<T, object>>[]? includes)
		{
			IQueryable<T> query = _repository.AsNoTracking();

			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (predicate is null) throw new BaseException("El filtro no puede ser nulo");

			var entity = await query.Where(predicate).Select(selector).FirstOrDefaultAsync();
			
			if (entity is null)
			{
				var type = typeof(T).Name;
				throw new BaseException($"No hay registros de tipo {type} para este filtro");
			}

			return entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync(bool ignoreFilter = false)
		{
			return (ignoreFilter) ? await _repository.IgnoreQueryFilters().ToListAsync() : await _repository.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _repository.FindAsync(id) ?? throw new BaseException($"No hay registros de tipo {typeof(T).Name} para este ID", System.Net.HttpStatusCode.NotFound);
		}

		public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null, bool ignoreFilter = false, params Expression<Func<T, object>>[]? includes)
		{
			IQueryable<T> query = ignoreFilter ? _repository.AsNoTracking().IgnoreQueryFilters() : _repository.AsNoTracking();

			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (predicate is not null) query = query.Where(predicate);

			IQueryable<TResult> query2 = query.Select(selector);

			if (orderBy is not null) query2 = orderBy(query2);
			
			var result = await query2.Take(5).ToListAsync();

			return result;
		}
		
		public async Task<PagedData<TResult>> GetListPaginateAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null, int page = 1, int pageSize = 10, params Expression<Func<T, object>>[]? includes) where TResult : class
		{
			IQueryable<T> query = _repository.AsNoTracking();

			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (predicate is not null) query = query.Where(predicate);

			IQueryable<TResult> query2 = query.Select(selector);

			if (orderBy is not null) query2 = orderBy(query2);
						
			var result = await query2.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
			
			return new PagedData<TResult>
			{
				Page = page,
				Size = pageSize,
				TotalCount = await _repository.CountAsync(predicate),
				Rows = result
			};
		}

		public async Task Update(T entity)
		{
			if (_context.Entry<T>(entity).State != EntityState.Detached) _context.Attach<T>(entity);

			_context.Entry(entity).State = EntityState.Modified;

			await CommitChangeAsync();
		}
	}
}
