using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record CreateDepositoDto : CreateNamedEntityDto
	{
        public required int DependenciaId { get; set; }
        public bool EsFuncion { get; set; }
    }
}
