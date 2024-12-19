using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Models.RegistroDebitoCredito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.CargoDescargo
{
    public record CreateRDC_Dto
	{
        // Informacion general
		public TipoDebitoCreditoEnum TipoDebito { get; set; }
		public TipoDebitoCreditoEnum TipoCredito { get; set; }
        public required string DebitoA { get; set; } 
		public required string CreditoA { get; set; }
		public required string Oficio { get; set; }
        public required string NoDocumento { get; set; }
        public DateOnly Fecha { get; set; }
        public required string Intendente { get; set; }
        public string? Observacion { get; set; }

        // Informacion articulos 
        public List<RegistroDebitoModel>? Articulos { get; set; }

		// Informacion de encargados
		public required string EncargadoArmas { get; set; }
		public required string EncargadoDepositos { get; set; }
	}
}
