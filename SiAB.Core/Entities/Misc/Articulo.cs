using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Articulos", Schema = "Misc")]
	public class Articulo : EntityMetadata, IAuditableEntityMetadata
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

        [ForeignKey(nameof(PropiedadId))]
		public int PropiedadId { get; set; }
		public virtual Propiedad? Propiedad { get; set; }
		public string? Serie { get; set; }
		public EstadoEnum Estado { get; set; }

        // Auditables

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
