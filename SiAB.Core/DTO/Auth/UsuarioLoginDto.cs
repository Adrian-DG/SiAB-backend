using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Auth
{
	public record UsuarioLoginDto
	{
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

}
