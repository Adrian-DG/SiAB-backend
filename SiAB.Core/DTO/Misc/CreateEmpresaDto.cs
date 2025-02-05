using SiAB.Core.Enums;
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
		public string? Numeracion { get; set; }
		public string? Titular { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? Archivo { get; set; }
		public DateOnly FechaEmision { get; set; }
		public DateOnly FechaVigencia { get; set; }
		public DateOnly FechaVencimiento { get; set; }
	}
}
