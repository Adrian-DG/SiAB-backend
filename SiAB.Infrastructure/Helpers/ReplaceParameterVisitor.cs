using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Helpers
{
	public class ReplaceParameterVisitor : ExpressionVisitor
	{
		private readonly ParameterExpression _oldParameter;
		private readonly ParameterExpression _newParameter;

		public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
		{
			_oldParameter = oldParameter;
			_newParameter = newParameter;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			return node == _oldParameter ? _newParameter : base.VisitParameter(node);
		}
	}
}
