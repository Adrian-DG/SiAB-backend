using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models
{
    public record GroupEntityModel
    {
		public required string Nombre { get; set; }
		public int Total { get; set; }
	}
}
