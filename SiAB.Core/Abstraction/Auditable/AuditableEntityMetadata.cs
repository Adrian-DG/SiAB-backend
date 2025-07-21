using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Abstraction.Auditable
{
	public abstract class AuditableEntityMetadata : EntityMetadata, IAuditableEntityMetadata
	{
		// auditables 

		// Creador

		[ForeignKey(nameof(UsuarioCreadorId))]
		public int UsuarioCreadorId { get; set; }
		public virtual Usuario? UsuarioCreador { get; set; }
		public InstitucionEnum UsuarioCreadorCodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }

		// Editor

		[ForeignKey(nameof(UsuarioEditorId))]
		public int? UsuarioEditorId { get; set; }
		public virtual Usuario? UsuarioEditor { get; set; }
		public InstitucionEnum UsuarioEditorCodInstitucion { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
