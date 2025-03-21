﻿
using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("Secuencias", Schema = "Belico")]
	public class Secuencia : EntityMetadata, IAuditableEntityMetadata
	{
		public required string SecuenciaCadena { get; set; }
		public int SecuenciaNumero { get; set; } = 1;
		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		
	}
}
