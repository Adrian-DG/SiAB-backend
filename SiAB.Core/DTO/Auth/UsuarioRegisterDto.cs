using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Auth
{
	public record UsuarioRegisterDto
	{
        public required string Cedula { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required int RangoId { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required IList<int> Roles { get; set; }
    }
}
