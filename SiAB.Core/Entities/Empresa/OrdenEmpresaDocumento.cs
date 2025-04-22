using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Empresa
{
	[Table("OrdenesEmpresaDocumentos", Schema = "EXP")]
	public class OrdenEmpresaDocumento : EntityMetadata, IAuditableEntityMetadata
	{
		public string? NombreArchivo { get; set; }
		public byte[]? Archivo { get; set; }

		public DateOnly FechaEmision { get; set; }
		public DateOnly FechaRecepcion { get; set; }
		public DateOnly FechaExpiracion { get; set; }


		[ForeignKey(nameof(TipoDocumentoId))]
		public int TipoDocumentoId { get; set; }
		public virtual TipoDocumento? TipoDocumento { get; set; }


		[ForeignKey(nameof(OrdenEmpresaId))]
		public int OrdenEmpresaId { get; set; }
		public virtual OrdenEmpresa? Orden { get; set; }

		public int UsuarioId { get; set; }
		public InstitucionEnum CodInstitucion { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? UsuarioIdModifico { get; set; }
		public DateTime? FechaModificacion { get; set; }

		[NotMapped]
		public bool EstaVencida
		{
			get
			{
				return FechaExpiracion < DateOnly.FromDateTime(DateTime.Now);
			}
		}

		[NotMapped]
		public string? DocumentDataUrl
		{
			get
			{
				if (Archivo == null) return null;
				string base64String = Convert.ToBase64String(Archivo);
				return $"data:application/pdf;filename={NombreArchivo};base64,{base64String}";
			}
		}

		[NotMapped]
		public int DiasRestantes
		{
			get
			{
				return (FechaExpiracion.ToDateTime(TimeOnly.MinValue) - DateTime.Now).Days;
			}
		}

	}
}
