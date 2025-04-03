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
		public required string Secuencia { get; set; }

		public required string RecibidoParam2 { get; set; }
		public required string RecibidoParam1 { get; set; }

		public required IMiembroListDetail Intendente { get; set; }
		public required IMiembroListDetail EncargadoArmas { get; set; }
		public required IMiembroListDetail EncargadoDepositos { get; set; }		

		public required string Fecha { get; set; }
		public string? Comentario { get; set; }
		public required List<ArticuloItemDto> Articulos { get; set; }
	}
}
