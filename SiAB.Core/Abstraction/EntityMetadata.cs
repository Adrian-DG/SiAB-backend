using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Abstraction
{
	public abstract class EntityMetadata
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public bool IsDeleted { get; set; } = false;
    }
}
