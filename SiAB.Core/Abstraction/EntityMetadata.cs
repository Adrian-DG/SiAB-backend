using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;

namespace SiAB.Core.Abstraction
{
	public abstract class EntityMetadata
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey(nameof(UsuarioId))]
		public Nullable<int> UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
	}
}
