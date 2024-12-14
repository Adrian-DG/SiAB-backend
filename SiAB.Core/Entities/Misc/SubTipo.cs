using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("SubTipos", Schema = "Misc")]
	public class SubTipo : NamedMetadata
	{
        [ForeignKey(nameof(TipoId))]
        public int TipoId { get; set; }
        public virtual Tipo? Tipo { get; set; }
    }
}
