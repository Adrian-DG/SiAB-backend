using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
    [Table("FotoModelos", Schema = "Misc")]
	public class FotoModelo : EntityMetadata
    { 
        public byte[]? Foto { get; set; }
        public virtual ICollection<Modelo>? Modelos { get; set; }
	}
}
