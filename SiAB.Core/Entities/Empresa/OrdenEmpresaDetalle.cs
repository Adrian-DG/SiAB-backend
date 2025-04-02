using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Inventario;
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
	[Table("OrdenesEmpresaDetalles", Schema = "EXP")]
	public class OrdenEmpresaDetalle : EntityMetadata, IAuditableEntityMetadata
	{

		[ForeignKey(nameof(OrdenEmpresaArticulo))]
		public int OrdenEmpresaArticulo { get; set; }
		public virtual OrdenEmpresaArticulo? Articulo { get; set; }

		[ForeignKey(nameof(OrdenEmpresaId))]
		public int OrdenEmpresaId { get; set; }
		public virtual OrdenEmpresa? Orden { get; set; }

		// Auditables 

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
