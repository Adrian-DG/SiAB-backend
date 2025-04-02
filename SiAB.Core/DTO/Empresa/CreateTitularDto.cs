using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateTitularDto
	{
		public required string Identificacion { get; set; }
		public required string Nombre { get; set; }
		public required string Apellido { get; set; }

	}
}
 