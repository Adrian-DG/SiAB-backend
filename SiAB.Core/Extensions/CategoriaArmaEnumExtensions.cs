using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Extensions
{
    public static class CategoriaArmaEnumExtensions
	{
		public static string GetDescription(this CategoriaArticuloEnum value)
		{
			return value switch
			{
				CategoriaArticuloEnum.NO_DEFINIDA => "NO DEFINIDA",
				CategoriaArticuloEnum.ARMAS => "ARMAS",
				CategoriaArticuloEnum.MUNICIONES => "MUNICIONES",
				CategoriaArticuloEnum.EXPLOSIVOS => "EXPLOSIVOS",
				CategoriaArticuloEnum.QUIMICOS => "QUIMICOS",
				CategoriaArticuloEnum.ACCESORIOS => "ACCESORIOS",
				CategoriaArticuloEnum.EQUIPOS => "EQUIPOS",
				_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
			};
		}
	}
}
