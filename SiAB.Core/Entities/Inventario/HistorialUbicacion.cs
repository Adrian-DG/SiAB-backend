using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Inventario
{
	[Table("HistorialUbicacion", Schema = "Inv")]
	public class HistorialUbicacion 
	{
		public required string Serie { get; set; }

		[ForeignKey(nameof(ArticuloId))]
		public int ArticuloId { get; set; }
		public virtual Articulo? Articulo { get; set; }

		[ForeignKey(nameof(TransaccionId))]
		public int TransaccionId { get; set; }
		public virtual Transaccion? Transaccion { get; set; }
	}
}
