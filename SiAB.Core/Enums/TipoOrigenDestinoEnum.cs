using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Enums
{
	public enum TipoOrigenDestinoEnum
	{
		[Description("Proveedor")]
		PROVEEDOR = 1,
		[Description("MILITAR")]
		MILITAR,
		[Description("CIVIL")]
		CIVIL,
		[Description("DEPOSITO")]
		DEPOSITO,
		[Description("FUNCION O CARGO")]
		FUNCION,
	}
}
