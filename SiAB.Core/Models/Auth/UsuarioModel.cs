using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Auth
{
	public class UsuarioModel
	{
		public int Id { get; set; }
		public string Cedula { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Rango { get; set; }
		public string Institucion { get; set; }
	}
}
