using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Articulos", Schema = "Misc")]
	public class Articulo : NamedMetadata
	{
		[ForeignKey(nameof(CategoriaId))]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        [ForeignKey(nameof(TipoId))]
        public int TipoId { get; set; }
        public virtual Tipo? Tipo { get; set; }

        [ForeignKey(nameof(SubTipoId))]
        public int SubTipoId { get; set; }
        public virtual SubTipo? SubTipo { get; set; }

        [ForeignKey(nameof(MarcaId))]
        public int MarcaId { get; set; }
        public virtual Marca? Marca { get; set; }

        [ForeignKey(nameof(ModeloId))]
        public int ModeloId { get; set; }
        public virtual Modelo? Modelo { get; set; }

        [ForeignKey(nameof(CalibreId))]
        public int CalibreId { get; set; }
        public virtual Calibre? Calibre { get; set; }

        public bool EsSeriado { get; set; } = false;

        public virtual ICollection<Serie>? Series { get; set; }
        public virtual ICollection<Alerta>? Alertas { get; set; }
    }
}
