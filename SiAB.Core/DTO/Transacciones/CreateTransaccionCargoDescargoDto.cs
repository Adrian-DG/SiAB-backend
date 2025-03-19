using SiAB.Core.DTO.Inventario;
using SiAB.Core.Enums;
using SiAB.Core.Models.Sipffaa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Transacciones
{
	public record CreateTransaccionCargoDescargoDto
	{
		public required string Secuencia { get; set; }
		public TipoOrigenDestinoEnum TipoCargoDebito { get; set; }
		public required string Debito { get; set; }

		public TipoOrigenDestinoEnum TipoCargoCredito { get; set; }
		public required string Credito { get; set; }

		public string? Documento { get; set; }		

		public string? Oficio { get; set; }
		public string? NoDocumento { get; set; }
		public required string Fecha { get; set; }
		public required string Intendente { get; set; }

		public string? Observaciones { get; set; }
		public required List<CreateArticuloItemDto> Articulos { get; set; }

		public required string EncargadoArmas { get; set; }
		public string? EncargadoDepositos { get; set; }
					 
		public string? Entrega { get; set; }
		public string? Recibe { get; set; }
		public string? Firma { get; set; }
	}
}
