using SiAB.Core.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.Empresa
{
	public sealed class OrdenEmpresaArticuloModel : ArticuloDetailModel
	{
		public int CantidadRecibida { get; set; }
		public int CantidadEntregada { get; set; }
	}
}
