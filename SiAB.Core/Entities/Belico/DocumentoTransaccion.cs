using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Belico
{
	[Table("DocumentosTransaccion", Schema = "Belico")]
	public class DocumentoTransaccion : EntityMetadata, IAuditableEntityMetadata
	{
		[ForeignKey(nameof(TransaccionId))]
		public int TransaccionId { get; set; }
		public virtual Transaccion? Transaccion { get; set; }

		[ForeignKey(nameof(TipoDocuemntoId))]
		public int TipoDocuemntoId { get; set; }
		public virtual TipoDocumento? TipoDocumento { get; set; }

		public required string NumeracionDocumento { get; set; }
		public byte[]? Archivo { get; set; }

		// auditables
		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}
