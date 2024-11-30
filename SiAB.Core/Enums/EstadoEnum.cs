using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Enums
{
	public enum EstadoEnum
	{
		[Description("NO DEFINIDO")]
		NO_DEFINIDO = 1,
		[Description("CONDICION OPTIMA")]
		OPTIMA,
		[Description("REGULAR")]
		REGULAR,
		[Description("EN MAL ESTADO")]
		EN_MAL_ESTADO
	}
}
