using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.RegistroDebitoCredito
{
    public class RegistroArticuloDebitoModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Serie { get; set; }
        public int Cantidad { get; set; }
    }
}
