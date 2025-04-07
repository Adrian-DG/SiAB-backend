using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Empresa
{
	[Table("OrdenesEmpresa", Schema = "EXP")]
	public class OrdenEmpresa : EntityMetadata, IAuditableEntityMetadata
	{
		public string? Comentario { get; set; }

		public DateOnly FechaEfectividad { get; set; }

		[ForeignKey(nameof(EmpresaId))]
		public int EmpresaId { get; set; }
		public virtual Empresa? Empresa { get; set; }
		public virtual ICollection<OrdenEmpresaDocumento>? Documentos { get; set; }
		public virtual ICollection<OrdenEmpresaArticulo>? Articulos { get; set; }

		// Auditables 

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
