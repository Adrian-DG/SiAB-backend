using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Empresa
{
	[Table("Contactos", Schema = "EXP")]
	public class Contacto : EntityMetadata
	{
		public required string Telefono { get; set; }

		[ForeignKey(nameof(EmpresaId))]
		public int EmpresaId { get; set; }
		public virtual Empresa? Empresa { get; set; }
	}
}
