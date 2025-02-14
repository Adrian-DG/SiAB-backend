using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.ProcedureResults
{
    public class SerieTransaccionItem
    {
		public string? Origen { get; set; }
		public string? Destino { get; set; }
		public string? Formulario { get; set; }
		public DateTime FechaEfectividad { get; set; }
	}
}
