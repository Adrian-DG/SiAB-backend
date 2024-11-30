using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	public class Modelo : NamedMetadata
	{
        [ForeignKey("MarcaId")]
        public int MarcaId { get; set; }
        public virtual Marca? Marca { get; set; }
    }
}
