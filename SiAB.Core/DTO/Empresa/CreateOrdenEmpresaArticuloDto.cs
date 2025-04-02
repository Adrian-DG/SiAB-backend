﻿using SiAB.Core.Entities.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.DTO.Empresa
{
	public record CreateOrdenEmpresaArticuloDto
	{	
		public int CategoriaId { get; set; }		
		public int TipoId { get; set; }
		public int SubTipoId { get; set; }
		public int MarcaId { get; set; }
		public int ModeloId { get; set; }
		public int CalibreId { get; set; }
		public string? Serie { get; set; }
		public int Cantidad { get; set; }
	}
}
