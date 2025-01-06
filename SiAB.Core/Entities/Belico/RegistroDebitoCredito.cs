
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
	public class RegistroDebitoCredito : EntityMetadata, IAuditableEntityMetadata
	{
		public IList<RegistroDebitoModel>? Articulos { get; set; }					
        public required string DebitoA { get; set; } // Recibea articulo
        public TipoDebitoCreditoEnum TipoDebito { get; set; }
        public required string CreditoA { get; set; } // Entrega articulo
		public TipoDebitoCreditoEnum TipoCredito  { get; set; }
        public required DateTime FechaEfectividad { get; set; }

        [ForeignKey(nameof(UsuarioId))]
		public required int UsuarioId { get; set; }
		public required InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime? FechaModificacion { get; set; } = null;

		[ForeignKey(nameof(UsuarioIdModifico))]
		public int? UsuarioIdModifico { get; set; }
	}
}
