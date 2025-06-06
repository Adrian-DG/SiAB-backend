﻿using SiAB.Core.DTO.Inventario;
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

		public TipoTransaccionEnum TipoTransaccion { get; set; }

		public required string NoDocumento { get; set; }
		public required string Documento { get; set; }		
		public required string Fecha { get; set; }
		public required string Intendente { get; set; }

		public required string Observaciones { get; set; }
		public required List<CreateArticuloItemDto> Articulos { get; set; }

		public required string EncargadoArmas { get; set; }
		public required string EncargadoDepositos { get; set; }
					 
	}
}
