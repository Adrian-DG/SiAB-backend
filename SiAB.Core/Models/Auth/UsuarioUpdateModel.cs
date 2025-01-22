using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Auth
{
	public record UsuarioUpdateModel
	{
		public string? Cedula { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public int RangoId { get; set; }
		public string[]? Roles { get; set; }
	}
}
