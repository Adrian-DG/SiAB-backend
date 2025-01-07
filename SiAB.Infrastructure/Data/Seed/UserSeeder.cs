using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data.Seed
{
	public static class UserSeeder
	{
		public static void Seed(ModelBuilder builder) 
		{
			var passwordHasher = new PasswordHasher<Usuario>();

			builder.Entity<Usuario>().HasData(
				new Usuario
				{
					Id = 1,
					Cedula = "0000000000",
					Nombre = "Admin",
					Apellido = "Admin",
					UserName = "admin",
					NormalizedUserName = "ADMIN",
					Institucion = InstitucionEnum.MIDE,
					PasswordHash = passwordHasher.HashPassword(null, "admin01")
				});

			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int> { UserId = 1, RoleId = 1 }
			);
		}
	}
}
