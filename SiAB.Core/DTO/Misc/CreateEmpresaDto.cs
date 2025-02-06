using SiAB.Core.Enums;
using SiAB.Core.Models.miscelaneos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record CreateEmpresaDto : CreateNamedEntityDto
	{
		public string? Telefono { get; set; }
		public string? RNC { get; set; }		
		public string? Titular { get; set; }
		public DocumentoEmpresaModel[]? DataArchivos { get; set; }

	}
}
