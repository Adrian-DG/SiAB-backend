﻿using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Misc
{
	public record CreateProveedorDto : CreateNamedEntityDto
	{
		public string? Telefono { get; set; }
		public string? RNC { get; set; }
		public string? Numeracion { get; set; }
		public TipoLicenciaEnum TipoLicencia { get; set; }
		public string? Archivo { get; set; }
		public DateTime FechaEmision { get; set; }
		public DateTime FechaVencimiento { get; set; }
	}
}
