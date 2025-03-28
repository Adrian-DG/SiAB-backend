using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Transacciones
{
	public class TransaccionViewModel
	{
		public int Id { get; set; }
		public string? Origen { get; set; }
		public string? Destino { get; set; }
		public string? Formulario { get; set; }
		public DateOnly Fecha { get; set; }
		public bool? Tiene53 { get; set; }
		public string? Usuario { get; set; }
		public DateTime FechaCreacion { get; set; }

	}
}
