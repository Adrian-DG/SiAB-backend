using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Empresa
{
	[Table("Titulares", Schema = "EXP")]
	public class Titular : EntityMetadata
	{
		public required string Identificacion { get; set; }
		public required string Nombre { get; set; }
		public required string Apellido { get; set; }

		[ForeignKey(nameof(EmpresaId))]
		public int EmpresaId { get; set; }
		public virtual Empresa? Empresa { get; set; }
	}
}
