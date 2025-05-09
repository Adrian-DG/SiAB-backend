﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Transacciones
{
	public record AdjuntarFormularioTransaccionDto
	{
		public int Id { get; set; }
		public int TipoDocumentoId { get; set; }
		public string? Numeracion { get; set; }
		public string? Url { get; set; }
	}
}
