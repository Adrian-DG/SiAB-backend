using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record UpdateNamedEntityDto
	{
        public required string Nombre { get; set; }
    }
}
