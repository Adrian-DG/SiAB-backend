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
using SiAB.Core.Enums;

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
				},
				new Usuario
				{
					Id = 2,
					Cedula = "0000000001",
					Nombre = "MIDE",
					Apellido = "MIDE",
					UserName = "mide",
					NormalizedUserName = "MIDE",
					Institucion = InstitucionEnum.MIDE,
					PasswordHash = passwordHasher.HashPassword(null, "mide01")
				},
				new Usuario
				{
					Id = 3,
					Cedula = "0000000002",
					Nombre = "ERD",
					Apellido = "ERD",
					UserName = "erd",
					NormalizedUserName = "ERD",
					Institucion = InstitucionEnum.ERD,
					PasswordHash = passwordHasher.HashPassword(null, "erd01")
				},
				new Usuario
				{
					Id = 4,
					Cedula = "0000000003",
					Nombre = "ARD",
					Apellido = "ARD",
					UserName = "ard",
					NormalizedUserName = "ARD",
					Institucion = InstitucionEnum.ARD,
					PasswordHash = passwordHasher.HashPassword(null, "ard01")
				},
				new Usuario
				{
					Id = 5,
					Cedula = "0000000004",
					Nombre = "FARD",
					Apellido = "FARD",
					UserName = "fard",
					NormalizedUserName = "FARD",
					Institucion = InstitucionEnum.FARD,
					PasswordHash = passwordHasher.HashPassword(null, "fard01")
				},
				new Usuario
				{
					Id = 6,
					Cedula = "0000000005",
					Nombre = "EXPLOSIVOS",
					Apellido = "EXPLOSIVOS",
					UserName = "explosivos",
					NormalizedUserName = "EXPLOSIVOS",
					Institucion = InstitucionEnum.MIDE,
					PasswordHash = passwordHasher.HashPassword(null, "explosivos01")
				}
				);

				

			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int> { UserId = 1, RoleId = (int)UsuarioRolesEnum.ADMINISTRADOR_GENERAL },
				new IdentityUserRole<int> { UserId = 2, RoleId = (int)UsuarioRolesEnum.ADMINISTRADOR_GENERAL },
				new IdentityUserRole<int> { UserId = 3, RoleId = (int)UsuarioRolesEnum.ADMINISTRADOR_GENERAL },
				new IdentityUserRole<int> { UserId = 4, RoleId = (int)UsuarioRolesEnum.ADMINISTRADOR_GENERAL },
				new IdentityUserRole<int> { UserId = 5, RoleId = (int)UsuarioRolesEnum.ADMINISTRADOR_GENERAL },
				
				new IdentityUserRole<int> { UserId = 6, RoleId = (int)UsuarioRolesEnum.MODULO_EMPRESAS },
				new IdentityUserRole<int> { UserId = 6, RoleId = (int)UsuarioRolesEnum.MODULO_MANTENIMIENTO },
				new IdentityUserRole<int> { UserId = 6, RoleId = (int)UsuarioRolesEnum.MANTENIMIENTO_TIPOS },
				new IdentityUserRole<int> { UserId = 6, RoleId = (int)UsuarioRolesEnum.MANTENIMIENTO_SUBTIPOS },
				new IdentityUserRole<int> { UserId = 6, RoleId = (int)UsuarioRolesEnum.MANTENIMIENTO_TIPOS_DOCUMENTOS }
			);
		}
	}
}
