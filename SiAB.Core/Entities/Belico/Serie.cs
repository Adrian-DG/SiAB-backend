using SiAB.Core.Abstraction;
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
	[Table("Series", Schema = "Belico")]
	public class Serie : EntityMetadata
    {
		[ForeignKey(nameof(ArticuloId))]
        public int ArticuloId { get; set; }
        public virtual Articulo? Articulo { get; set; }
        
        public string? SerieCode { get; set; }
        public EstadoEnum Estado { get; set; }

        [ForeignKey(nameof(PropiedadId))]
        public int PropiedadId { get; set; }
        public virtual Propiedad? Propiedad { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Comentario { get; set; }        
        public virtual ICollection<Alerta>? Alertas { get; set; }		
	}
}
