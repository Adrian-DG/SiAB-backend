using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Inventario
{
	public record CreateArticuloItemDto
	{
		public int Id { get; set; }
		public required string Marca { get; set; }
		public required string Modelo { get; set; }
		public required string SubTipo { get; set; }
		public required string Serie { get; set; }
		public int Cantidad { get; set; }
	}
}
