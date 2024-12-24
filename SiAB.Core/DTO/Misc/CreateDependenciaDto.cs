using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record CreateDependenciaDto : CreateNamedEntityDto
	{
        public bool EsExterna { get; set; }
        public InstitucionEnum InstitucionEnum { get; set; }
    }
}
