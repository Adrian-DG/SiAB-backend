
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using SiAB.Core.Enums;

namespace SiAB.Core.Entities.Auth
{
    [Table("Usuarios", Schema = "accesos")]
    public class Usuario : IdentityUser<int>
    {
        [Required]
        [StringLength(11)]
        public required string Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        [ForeignKey(nameof(RangoId))]
        public Nullable<int> RangoId { get; set; }
        public virtual Rango? Rango { get; set; }
        public InstitucionEnum Institucion { get; set; }
    }
}
