using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models
{
	public class OrdenEmpresaModel
	{
		public int Id { get; set; }
		public string? Comentario { get; set; }
		public DateOnly FechaEfectividad { get; set; }
		public int CantidadEntregada { get; set; }
		public int CantidadRecibida { get; set; }
		public int Documentos { get; set; }
	}
}
