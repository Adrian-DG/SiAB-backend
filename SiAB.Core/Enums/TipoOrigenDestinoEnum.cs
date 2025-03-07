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
		[Description("MILITAR")]
		MILITAR = 1,
		[Description("CIVIL")]
		CIVIL,
		[Description("DEPOSITO")]
		DEPOSITO,
		[Description("FUNCION O CARGO")]
		FUNCION,
	}
}
