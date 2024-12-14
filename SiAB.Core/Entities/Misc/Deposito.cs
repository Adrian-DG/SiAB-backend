using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Personal;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
	[Table("Depositos", Schema = "Misc")]
	public class Deposito : NamedMetadata
	{
		[ForeignKey(nameof(DependenciaId))]
		public int DependenciaId { get; set;}
		public virtual Dependencia? Dependencia { get; set;}

		[ForeignKey(nameof(FuncionId))]
		public int FuncionId { get; set;}	
		public virtual Funcion? Funcion { get; set; }
	}
}
