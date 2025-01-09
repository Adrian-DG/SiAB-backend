using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record UpdateModeloDto : UpdateNamedEntityDto
	{
		public string? Foto { get; set; }
		public int MarcaId { get; set; }
	}
}
