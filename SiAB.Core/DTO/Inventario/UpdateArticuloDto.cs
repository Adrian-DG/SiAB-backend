using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Inventario
{
	public record UpdateArticuloDto
	{
		public int CategoriaId { get; set; }
		public int TipoId { get; set; }
		public int SubTipoId { get; set; }
		public int MarcaId { get; set; }
		public int ModeloId { get; set; }
		public string? Serie { get; set; }
	}
}
