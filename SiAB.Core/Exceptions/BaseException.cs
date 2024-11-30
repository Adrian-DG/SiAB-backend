using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Exceptions
{
	public class BaseException : Exception
	{
		public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
		{
		}
	}
}
