﻿using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Calibres", Schema = "Misc")]
    public class Calibre : NamedEntityMetadata
    {
		public virtual ICollection<Articulo>? Articulos { get; set; }
	}
}
