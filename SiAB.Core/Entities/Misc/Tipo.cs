using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Tipos", Schema = "misc")]
	public class Tipo : NamedMetadata
	{
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
