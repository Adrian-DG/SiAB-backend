using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.CargoDescargo
{
	public record CreateCargoDescargoDto
	{
        // Informacion general
		public TipoCargoDescargoEnum TipoDebito { get; set; }
		public TipoCargoDescargoEnum TipoCredito { get; set; }
        public required string Oficio { get; set; }
        public required string NoDocumento { get; set; }
        public DateOnly Fecha { get; set; }
        public required string Intendente { get; set; }
        public string? Observacion { get; set; }

        // Informacion articulos 
        public List<int>? ArticuloId { get; set; }

		// Informacion de encargados
		public required string EncargadoArmas { get; set; }
		public required string EncargadoDepositos { get; set; }
	}
}
