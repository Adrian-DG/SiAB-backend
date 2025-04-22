using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Empresa
{
	public class OrdenEmpresaDocumentoDetailModel
	{
		public int Id { get; set; }
		public string? NombreArchivo { get; set; }
		public string? DataUrl { get; set; }
		public string? TipoDocumento { get; set; }
		public DateOnly FechaEmision { get; set; }
		public DateOnly FechaRecepcion { get; set; }
		public DateOnly FechaExpiracion { get; set; }
	}
}
