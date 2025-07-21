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
	public interface IAuditableEntityMetadata
	{
		public int UsuarioCreadorId { get; set; }
		public InstitucionEnum UsuarioCreadorCodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }

		public int? UsuarioEditorId { get; set; }
		public InstitucionEnum UsuarioEditorCodInstitucion { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
