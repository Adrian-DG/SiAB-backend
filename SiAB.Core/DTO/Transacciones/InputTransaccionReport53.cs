using SiAB.Core.DTO.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Transacciones
{
    public record InputTransaccionReport53
    {
		public required string RecibidoPor { get; set; }
		public required string Secuencia { get; set; }
		public required string Intendente { get; set; }
		public required string EncargadoArmas { get; set; }
		public required string EncargadoDepositos { get; set; }
		public required string Fecha { get; set; }
		public required List<CreateArticuloItemDto> Articulos { get; set; }
		public string? Comentarios { get; set; }
	}
}
