using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Helpers
{
	public static class PredicateHelper
	{
		public static Expression<Func<T, bool>> CombinePredicates<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			var parameter = Expression.Parameter(typeof(T));
			var combined = new ReplaceParameterVisitor(first.Parameters[0], parameter).Visit(first.Body);
			combined = Expression.AndAlso(combined, new ReplaceParameterVisitor(second.Parameters[0], parameter).Visit(second.Body));
			return Expression.Lambda<Func<T, bool>>(combined, parameter);
		}
	}
}
