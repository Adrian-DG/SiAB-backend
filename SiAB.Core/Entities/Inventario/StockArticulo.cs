using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Inventario
{
	[Table("Stock", Schema = "Inv")]
	public class StockArticulo
	{
		[ForeignKey(nameof(ArticuloId))]	
		public int ArticuloId { get; set; }
		public virtual Articulo? Articulo { get; set; }

		[ForeignKey(nameof(IdDeposito))]
		public int IdDeposito { get; set; }
		public virtual Deposito? Deposito { get; set; }
		public int Cantidad { get; set; }
	}
}
