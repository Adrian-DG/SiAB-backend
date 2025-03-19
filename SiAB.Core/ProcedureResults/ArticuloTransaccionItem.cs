using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.ProcedureResults
{
	public sealed class ArticuloTransaccionItem
	{
		public int Id { get; set; }
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string SubTipo { get; set; }
		public string Calibre { get; set; }
		public string Serie { get; set; }
		public int Cantidad { get; set; }
		public string Formulario { get; set; }
		public DateTime? FechaEfectividad { get; set; }
	}
}
