using SiAB.Core.DTO.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateEmpresaDto : CreateNamedEntityDto
	{
		public required string RNC { get; set; }
		public List<CreateTitularDto>? Titulares { get; set; }
		public List<CreateContactoDto>? Telefonos { get; set; }
	}
}
