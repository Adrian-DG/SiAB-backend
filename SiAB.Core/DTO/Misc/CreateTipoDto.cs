using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record CreateTipoDto : CreateNamedEntityDto
	{
        public int CategoriaId { get; set; }
    }
}
