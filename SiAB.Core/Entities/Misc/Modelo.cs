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
	[Table("Modelos", Schema = "Misc")]
    public class Modelo : NamedEntityMetadata
    {
		public string? Foto { get; set; }

		[ForeignKey(nameof(MarcaId))]
        public int MarcaId { get; set; }
        public virtual Marca? Marca { get; set; }

        public virtual ICollection<Articulo>? Articulos { get; set; }
    }
}
