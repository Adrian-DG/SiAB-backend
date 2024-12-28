using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
    [Table("Marcas", Schema = "Misc")]
    public class Marca : NamedEntityMetadata
    {
        public virtual ICollection<Modelo>? Modelos { get; set; }
	}
}
