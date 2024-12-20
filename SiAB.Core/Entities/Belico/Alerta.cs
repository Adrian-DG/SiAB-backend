﻿using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("Alertas", Schema = "Belico")]
	public class Alerta : EntityMetadata
	{
        public string? Cedula { get; set; }
        public AlertaEstatusEnum Estatus { get; set; }

        [ForeignKey(nameof(SerieId))]
        public int SerieId { get; set; }
        public virtual Serie? Serie { get; set; }

        [ForeignKey(nameof(ArticuloId))]
        public int ArticuloId { get; set; }
        public virtual Articulo? Articulo { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Comentario { get; set; }

        [Required]
        public DateTime FechaEfectividad { get; set; }
    }
}
