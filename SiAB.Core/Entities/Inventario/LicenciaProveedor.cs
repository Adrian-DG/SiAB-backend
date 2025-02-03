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
	public class LicenciaProveedor : EntityMetadata, IAuditableEntityMetadata
	{
		public TipoLicenciaEnum TipoLicencia { get; set; }
		public required string Numeracion { get; set; }
		public byte[]? Archivo { get; set; }

		[ForeignKey(nameof(ProveedorId))]
		public int ProveedorId { get; set; }
		public virtual Proveedor? Proveedor { get; set; }

		public DateOnly FechaEmision { get; set; }
		public DateOnly FechaVencimiento { get; set; }

		// auditables
		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }

		[NotMapped]
		public int DiasRestantes => (DateTime.Parse(FechaVencimiento.ToString("dd-MM-yyyy")) - DateTime.Now).Days;

	}
}
