using Azure;
using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Auth;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Role> _repository;
		public RoleRepository(AppDbContext context)
		{
			_context = context;
			_repository = context.Set<Role>();
		}

		public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<Role, bool>> predicate, Expression<Func<Role, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null)
		{
			IQueryable<Role> query = _repository;			

			IQueryable<TResult> query2 = query.Where(predicate).Select(selector); //.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			if (orderBy is not null)
			{
				query2 = orderBy(query2);
			}

			var result = await query2.ToListAsync();

			return result;
		}
	}
}
