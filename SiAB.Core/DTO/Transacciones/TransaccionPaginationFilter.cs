using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Transacciones
{
	public record TransaccionPaginationFilter : PaginationFilter
	{
		public string? Origen { get; set; }
		public string? Destino { get; set; }
		public string? Formulario53 { get; set; }
		public DateOnly? FechaInicial { get; set; }
		public DateOnly? FechaFinal { get; set; }
	}
}
