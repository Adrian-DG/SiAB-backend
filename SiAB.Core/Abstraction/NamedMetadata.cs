using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Abstraction
{
	public abstract class NamedEntityMetadata : EntityMetadata
	{
        public required string Nombre { get; set; }
    }
}
