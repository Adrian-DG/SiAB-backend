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
	[Table("Empresas", Schema = "Misc")]
	public class Empresa : NamedEntityMetadata, IAuditableEntityMetadata
	{
		public string? RNC { get; set; }
		public string? Telefono { get; set; }
		public string? Titular { get; set; }

		public int UsuarioId { get  ; set  ; }
		public InstitucionEnum CodInstitucion { get  ; set  ; }
		public DateTime FechaCreacion { get  ; set  ; }
		public int? UsuarioIdModifico { get  ; set  ; }
		public DateTime? FechaModificacion { get  ; set  ; }
	}
}
