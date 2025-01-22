using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Auth
{
	public record UsuarioUpdateDto
	{
		public required string Cedula { get; set; }
		public required string Nombre { get; set; }
		public required string Apellido { get; set; }
		public int? RangoId { get; set; }
		public int[]? Roles { get; set; }
	}
}
