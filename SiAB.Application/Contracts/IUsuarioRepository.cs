using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IUsuarioRepository 
	{
		Task<PagedData<TResult>> GetListPaginateAsync<TResult>(Expression<Func<Usuario, bool>> predicate, 
			Expression<Func<Usuario, TResult>> selector, 
			Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? orderBy = null,
			int page = 1, int pageSize = 10, 
			params Expression<Func<Usuario, object>>[] includes) where TResult : class;
	}
}
