using SiAB.Core.DTO.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateOrdenEmpresaDto
	{
		public string? Comentario { get; set; }
		public required string FechaEfectividad { get; set; }
		public List<CreateOrdenEmpresaArticuloDto>? Articulos { get; set; }
		public List<CreateOrdenEmpresaDocumentoDto>? Documentos { get; set; }

	}
}
