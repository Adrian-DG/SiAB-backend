using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Auth
{
	public class AuthenticatedResponse
	{
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
