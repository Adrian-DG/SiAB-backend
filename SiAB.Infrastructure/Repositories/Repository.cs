﻿using Microsoft.EntityFrameworkCore;
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

		public async Task AddAsync(T entity)
		{
			await _repository.AddAsync(entity);
			await CommitChangeAsync();
		}

		public async Task CommitChangeAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ConfirmExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await _repository.AnyAsync(predicate);
		}

		public async Task DeleteById(int id)
		{
			var entity = await GetByIdAsync(id);

			if (entity is null) return;

			_repository.Remove(entity);

			await CommitChangeAsync();
		}

		public async Task<TResult> FindWhereAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
		{
			if (predicate is null) throw new BaseException("El filtro no puede ser nulo");

			var entity = await _repository.Where(predicate).Select(selector).FirstOrDefaultAsync();
			
			if (entity is null)
			{
				var type = typeof(T).Name;
				throw new BaseException($"No hay registros de tipo {type} para este filtro");
			}

			return entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _repository.FindAsync(id) ?? throw new BaseException($"No hay registros de tipo {typeof(T).Name} para este ID", System.Net.HttpStatusCode.NotFound);
		}

		public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
		{
			return await _repository.Where(predicate).Select(selector).ToListAsync();
		}

		public async Task<IEnumerable<TResult>> GetListPaginateAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, int page = 1, int pageSize = 10)
		{
			return await _repository.Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();
		}

		public async Task Update(T entity)
		{
			if (_context.Entry<T>(entity).State != EntityState.Detached) _context.Attach<T>(entity);

			_context.Entry(entity).State = EntityState.Modified;

			await CommitChangeAsync();
		}
	}
}
