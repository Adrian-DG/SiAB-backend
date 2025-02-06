using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.miscelaneos
{
	public record DocumentoEmpresaModel
	{
		public string? Numeracion { get; set; }
		public int TipoDocumentoId { get; set; }
		public string? Archivo { get; set; }
		public DateTime FechaEmision { get; set; }
		public DateTime FechaVigencia { get; set; }
		public DateTime FechaVencimiento { get; set; }
	}
}
