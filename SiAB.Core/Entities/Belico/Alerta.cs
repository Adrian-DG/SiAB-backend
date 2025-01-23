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
	[Table("Alertas", Schema = "Belico")]
	public class Alerta : EntityMetadata, IAuditableEntityMetadata
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

        [ForeignKey(nameof(UsuarioId))]
		public required int UsuarioId { get; set; }
		public required InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; } = DateTime.Now;

		[ForeignKey(nameof(UsuarioIdModifico))]
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; } = null;
		
	}
}
