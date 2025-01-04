using SiAB.Core.Abstraction;
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
        [ForeignKey(nameof(MarcaId))]
        public int MarcaId { get; set; }
        public virtual Marca? Marca { get; set; }

        [ForeignKey(nameof(FotoId))]
        public int? FotoId { get; set; }
        public virtual FotoModelo? Foto { get; set; }

        public virtual ICollection<Articulo>? Articulos { get; set; }
    }
}
