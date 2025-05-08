using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Inventario
{
	public class ArticuloDetailModel
	{
		public int Id { get; set; }
		public required string Categoria { get; set; }
		public required string Tipo { get; set; }
		public required string SubTipo { get; set; }
		public required string Marca { get; set; }
		public required string Calibre { get; set; }
		public string? Serie { get; set; }
		
	}
}
