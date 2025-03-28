using Microsoft.AspNetCore.Http;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Transacciones
{
	public record InputOrigenDestinoDto
	{
		public TipoOrigenDestinoEnum Origen { get; set; }
		public TipoOrigenDestinoEnum Destino { get; set; }
	}
}
