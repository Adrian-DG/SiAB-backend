using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Entities.Belico;

namespace SiAB.Core.Entities.Misc
{
	[Table("Propiedades", Schema = "Misc")]
	public class Propiedad : NamedEntityMetadata
	{
		public virtual ICollection<Serie>? Series { get; set; }
	}
}
