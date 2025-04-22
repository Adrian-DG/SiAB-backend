using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("Transacciones", Schema = "Belico")]
	public class Transaccion : EntityMetadata, IAuditableEntityMetadata
	{
		public TipoOrigenDestinoEnum TipoOrigen { get; set; }
		public required string Origen { get; set; }

		public TipoOrigenDestinoEnum TipoDestino { get; set; }
		public required string Destino { get; set; }

		public TipoTransaccionEnum TipoTransaccion { get; set; }

		[StringLength(11)]
		public required string Intendente { get; set; }

		[StringLength(11)]
		public required string EncargadoGeneral { get; set; }
		
		[StringLength(11)]
		public required string EncargadoDeposito { get; set; }

		public DateOnly FechaEfectividad { get; set; }

		[DefaultValue(EstatusTransaccionEnum.EN_PROCESO)]
		public EstatusTransaccionEnum Estatus { get; set; } = EstatusTransaccionEnum.EN_PROCESO;

		// auditables 

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }

		public virtual ICollection<DetalleArticuloTransaccion>? DetallesTransaccion { get; set; }
		public virtual ICollection<DocumentoTransaccion>? DocumentosTransaccion { get; set; }
	}
}
