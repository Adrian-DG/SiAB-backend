using SiAB.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IRoleRepository
	{
		Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<Role, bool>> predicate, Expression<Func<Role, TResult>> selector, Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null);
	}
}
