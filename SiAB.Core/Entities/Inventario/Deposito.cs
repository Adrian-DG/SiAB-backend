﻿using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Personal;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Inventario
{
	[Table("Depositos", Schema = "Inv")]
	public class Deposito : NamedEntityMetadata
	{
		public bool EsFuncion { get; set; } = false;

		[ForeignKey(nameof(DependenciaId))]
		public int DependenciaId { get; set; }
		public virtual Dependencia? Dependencia { get; set; }
	}
}
