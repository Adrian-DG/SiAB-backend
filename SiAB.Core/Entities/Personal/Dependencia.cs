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
	[Table("Dependencias", Schema = "Personal")]
	public class Dependencia : NamedMetadata
	{
		public virtual ICollection<Funcion>? Funciones { get; set; }
        public InstitucionEnum Institucion { get; set; }
        public bool EsExterna { get; set; }
    }

}
