using SiAB.Core.Abstraction;
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
	[Table("Alertas", Schema = "belico")]
	public class Alerta : EntityMetadata
	{
		[Required]
		[StringLength(11)]
        public string Cedula { get; set; }

		[ForeignKey("SerieId")]
        public int SerieId { get; set; }
        public string? Nota { get; set; }
        public AlertaEstatusEnum Estatus { get; set; }
        public string? Origen { get; set; }      

    }
}
