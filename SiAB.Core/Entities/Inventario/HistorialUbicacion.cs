using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Inventario
{
	[Table("HistorialUbicacion", Schema = "Inv")]
	public class HistorialUbicacion : EntityMetadata, IAuditableEntityMetadata
	{
		[ForeignKey(nameof(ArticuloId))]
		public int ArticuloId { get; set; }
		public virtual Articulo? Articulo { get; set; }
		public TipoTransaccionEnum TipoTransaccion { get; set; }
		public TipoOrigenDestinoEnum TipoOrigen { get; set; }
		public required string Origen { get; set; }
		public TipoOrigenDestinoEnum TipoDestino { get; set; }
		public required string Destino { get; set; }

		[ForeignKey(nameof(TransaccionId))]
		public int TransaccionId { get; set; }
		public virtual Transaccion? Transaccion { get; set; }

		// auditables
		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
