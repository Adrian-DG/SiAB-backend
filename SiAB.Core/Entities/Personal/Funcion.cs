using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Personal
{
	[Table("Funciones", Schema = "Personal")]
	public class Funcion : NamedMetadata
	{
		[ForeignKey(nameof(DependenciaId))]
        public int DependenciaId { get; set; }
        public virtual Dependencia? Dependencia { get; set; }
    }
}
