using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Categorias", Schema = "Misc")]
	public class Categoria : NamedEntityMetadata
	{
		public virtual ICollection<Tipo>? Tipos { get; set; }
		public virtual ICollection<Articulo>? Articulos { get; set; }
	}
}
