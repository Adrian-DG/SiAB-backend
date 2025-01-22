using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Sipffaa
{
	public class MiembroListQueryResult
	{
		public byte[]? Foto { get; set; }
		public string? Rango { get; set; }
		public string? Cedula { get; set; }
		public string? NombreApellidoCompleto { get; set; }
		public string? EstadoMiembro { get; set; }
	}
}
