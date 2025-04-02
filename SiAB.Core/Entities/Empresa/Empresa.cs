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
	[Table("Empresas", Schema = "EXP")]
	public class Empresa : EntityMetadata, IAuditableEntityMetadata
	{
		public required string Nombre { get; set; }
		public required string RNC { get; set; }

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }

		public virtual ICollection<Titular>? Titulares { get; set; }
		public virtual ICollection<Contacto>? Contactos { get; set; }
		public virtual ICollection<OrdenEmpresa>? OrdenesEmpresa { get; set; }
	}
}
