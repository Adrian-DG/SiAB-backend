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
	[Table("SubTipos", Schema = "Misc")]
	public class SubTipo : NamedEntityMetadata
	{
        [ForeignKey(nameof(TipoId))]
        public int TipoId { get; set; }
        public virtual Tipo? Tipo { get; set; }
		public virtual ICollection<Articulo>? Articulos { get; set; }
	}
}
