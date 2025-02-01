using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.ProcedureResults
{
	public sealed class ArticuloTransaccionItem
	{
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string SubTipo { get; set; }
		public string Serie { get; set; }
		public int Cantidad { get; set; }
		public string Formulario { get; set; }
	}
}
