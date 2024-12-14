using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Categorias", Schema = "Misc")]
	public class Categoria : NamedMetadata
	{
		public virtual ICollection<Tipo>? Tipos { get; set; }
	}
}
