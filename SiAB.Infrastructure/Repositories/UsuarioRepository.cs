using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Models;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Usuario> _repository;
		public UsuarioRepository(AppDbContext context)
		{
			_context = context;
			_repository = context.Set<Usuario>();
		}

		public async Task<PagedData<TResult>> GetListPaginateAsync<TResult>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null, int page = 1, int pageSize = 10, params Expression<Func<Usuario, object>>[] includes) where TResult : class
		{
			IQueryable<Usuario> query = _repository;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			IQueryable<TResult> query2 = query.Where(predicate).Select(selector); //.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			if (orderBy is not null)
			{
				query2 = orderBy(query2);
			}


			var result = await query2.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			return new PagedData<TResult>
			{
				Page = page,
				Size = pageSize,
				TotalCount = await _repository.CountAsync(predicate),
				Rows = result
			};
		}
	}
}
