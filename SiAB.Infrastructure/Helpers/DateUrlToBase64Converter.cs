using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Helpers
{
    public static class DateUrlToBase64Converter
    {
		public static string ConvertDataUrlToBase64String(string dataUrl)
		{
			// Verificar si el dataUrl contiene la parte base64
			if (dataUrl.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
			{
				// Buscar la coma que separa los metadatos de los datos
				var base64Index = dataUrl.IndexOf("base64,", StringComparison.OrdinalIgnoreCase);
				if (base64Index >= 0)
				{
					// Extraer la parte base64
					var base64Data = dataUrl.Substring(base64Index + 7);
					return base64Data;
				}
			}

			throw new ArgumentException("El dataUrl proporcionado no es válido o no contiene datos base64.");
		}
	}
}
