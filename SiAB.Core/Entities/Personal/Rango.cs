using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Personal
{
	[Table("Rangos", Schema = "Personal")]
	public class Rango : NamedEntityMetadata
	{
        public string? NombreArmada { get; set; }
    }
}
