
using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Models.RegistroDebitoCredito;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
    [Table("Registros_Cargo_Descago", Schema = "Belico")]
	public class RegistroDebitoCredito : EntityMetadata
	{
		public IList<RegistroDebitoModel>? Articulos { get; set; }					
        public required string DebitoA { get; set; } // Recibea articulo
        public TipoDebitoCreditoEnum TipoDebito { get; set; }
        public required string CreditoA { get; set; } // Entrega articulo
		public TipoDebitoCreditoEnum TipoCredito  { get; set; }
    }
}
