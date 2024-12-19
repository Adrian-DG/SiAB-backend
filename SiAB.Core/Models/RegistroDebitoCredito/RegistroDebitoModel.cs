using SiAB.Core.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.RegistroDebitoCredito
{
    public class RegistroDebitoModel
    {
        public int ArticuloId { get; set; }
        public virtual Articulo? Articulo { get; set; }
        public string? Serie { get; set; }
        public int Cantidad { get; set; }
    }
}
