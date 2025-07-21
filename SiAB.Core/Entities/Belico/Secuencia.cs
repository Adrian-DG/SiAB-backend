
using SiAB.Core.Abstraction;
using SiAB.Core.Abstraction.Auditable;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("Secuencias", Schema = "Belico")]
	public class Secuencia : AuditableEntityMetadata
	{
		public required string SecuenciaCadena { get; set; }
		public int SecuenciaNumero { get; set; } = 1;

	}
}
