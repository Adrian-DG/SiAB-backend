using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("DetallesArticuloTransaccion", Schema = "Belico")]
	public class DetalleArticuloTransaccion : EntityMetadata, IAuditableEntityMetadata
	{

		[ForeignKey(nameof(ArticuloId))]
		public int ArticuloId { get; set; }
		public virtual Articulo? Articulo { get; set; }
		public int Cantidad { get; set; } 

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
