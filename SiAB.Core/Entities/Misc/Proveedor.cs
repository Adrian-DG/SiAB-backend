using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Proveedores", Schema = "Misc")]
	public class Proveedor : NamedEntityMetadata
	{
		public string? RNC { get; set; }
		public string? Telefono { get; set; }
	}
}
