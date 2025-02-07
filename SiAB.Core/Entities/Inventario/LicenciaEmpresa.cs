using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Inventario
{
	[Table("Licencias", Schema = "Inv")]
	public class LicenciaEmpresa : EntityMetadata, IAuditableEntityMetadata
	{
		[ForeignKey(nameof(TipoDocumentoId))]
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento? TipoDocumento { get; set; }
		public required string Numeracion { get; set; }
		public string? Archivo { get; set; }

		[ForeignKey(nameof(EmpresaId))]
		public int EmpresaId { get; set; }
		public virtual Empresa? Proveedor { get; set; }

		public DateOnly FechaEmision { get; set; }
        public DateOnly FechaVigencia { get; set; }
        public DateOnly FechaVencimiento { get; set; }

		// auditables
		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }

        public LicenciaEstatusEnum LicenciaEstatusEnum { get; set; }

        [NotMapped]
		public int DiasDeVigencia => FechaVencimiento.DayNumber - FechaVigencia.DayNumber;

		[NotMapped]
		public int DiasRestantes => FechaVencimiento.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;

	}
}
