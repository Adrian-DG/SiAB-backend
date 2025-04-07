using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateOrdenEmpresaDocumentoDto
	{
		public int TipoDocumentoId { get; set; }
		public string? Nombre { get; set; }
		public string? Archivo { get; set; }
		public string? FechaEmision { get; set; }
		public string? FechaDuracion { get; set; }
		public string? FechaExpiracion { get; set; }

	}
}
