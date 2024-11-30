﻿using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Misc
{
    [Table("Rangos", Schema = "misc")]
	public class Rango : NamedMetadata
	{
        public string NombreArmada { get; set; }
    }
}
