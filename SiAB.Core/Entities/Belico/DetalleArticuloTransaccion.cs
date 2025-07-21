using SiAB.Core.Abstraction;
using SiAB.Core.Abstraction.Auditable;
using SiAB.Core.Entities.Inventario;
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
	public class DetalleArticuloTransaccion : AuditableEntityMetadata
	{

		[ForeignKey(nameof(ArticuloId))]
		public int ArticuloId { get; set; }
		public virtual Articulo? Articulo { get; set; }	

		[ForeignKey(nameof(TransaccionId))]
		public int TransaccionId { get; set; }
		public virtual Transaccion? Transaccion { get; set; }
		public int Cantidad { get; set; }

	}
}
