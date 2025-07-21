using SiAB.Core.Abstraction.Auditable;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiAB.Core.Entities.Belico
{
	[Table("Alertas", Schema = "Belico")]
	public class Alerta : AuditableEntityMetadata
	{
        public string? Cedula { get; set; }
        public AlertaEstatusEnum Estatus { get; set; }

        [ForeignKey(nameof(ArticuloId))]
        public int ArticuloId { get; set; }
        public virtual Articulo? Articulo { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Comentario { get; set; }

        [Required]
        public DateTime FechaEfectividad { get; set; }	

	}
}
