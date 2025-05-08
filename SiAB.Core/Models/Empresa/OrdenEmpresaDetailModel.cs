using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Empresa
{
	public class OrdenEmpresaDetailModel
	{
		public int Id { get; set; }
		public string? Comentario { get; set; }
		public DateOnly FechaEfectividad { get; set; }
		public List<OrdenEmpresaArticuloModel>? Articulos { get; set; }
		public List<OrdenEmpresaDocumentoDetailModel>? Documentos { get; set; }
	}
}
