using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Inventario
{
	public record ArticuloItemDto : CreateArticuloItemDto
	{
		public string? Calibre { get; set; }
	}
}
