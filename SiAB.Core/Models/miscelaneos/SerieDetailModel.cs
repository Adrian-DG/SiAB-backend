using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models.miscelaneos
{
	public class SerieDetailModel
	{
        public int Id { get; set; }
        public string Serie { get; set; }
        public string Articulo { get; set; }
        public string Propiedad { get; set; }
        public string Comentario { get; set; }
    }
}
