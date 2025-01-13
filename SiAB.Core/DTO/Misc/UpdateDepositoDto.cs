using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record UpdateDepositoDto : UpdateNamedEntityDto
	{
		public required bool EsFuncion { get; set; }
		public required int DependenciaId { get; set; }
	}
}
