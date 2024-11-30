using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Entities.Auth
{
	[Table("Roles", Schema = "accesos")]
	public class Role : IdentityRole<int>
	{
        public string? Descripcion { get; set; }
    }
}
