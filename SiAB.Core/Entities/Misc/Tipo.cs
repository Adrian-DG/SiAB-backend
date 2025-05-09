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
	[Table("Tipos", Schema = "Misc")]
	public class Tipo : NamedEntityMetadata
	{
        [ForeignKey(nameof(CategoriaId))]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public virtual ICollection<SubTipo>? SubTipos { get; set; }
		public virtual ICollection<Articulo>? Articulos { get; set; }
	}
}
