using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Empresa
{
	[Table("OrdenesEmpresaArticulos", Schema = "EXP")]
	public class OrdenEmpresaArticulo : EntityMetadata, IAuditableEntityMetadata
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
		public string? Serie { get; set; }

		public int CantidadRecibida { get; set; }
		public int CantidadEntregada { get; set; } = 0;

		[ForeignKey(nameof(OrdenEmpresaId))]
		public int OrdenEmpresaId { get; set; }
		public virtual OrdenEmpresa? OrdenEmpresa { get; set; }

		// Auditables

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
