using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateContactoDto
	{
		public string? Telefono { get; set; }
	}
}
