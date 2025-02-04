using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data.Seed
{
	public static class RoleSeeder
	{
		public static void Seed(this ModelBuilder builder)
		{
			builder.Entity<Role>().HasData(
				new Role { Id = 1, Name = "ADMINISTRADOR GENERAL", NormalizedName = "ADMINISTRADOR GENERAL", Descripcion = "Acceso global a todos los módulos del sistema." },
				new Role { Id = 2, Name = "CONSULTA GENERAL", NormalizedName = "CONSULTA GENERAL", Descripcion = "Modulo para consulta de existencia por miembro, civil, deposito y función(S4)." },
				new Role { Id = 3, Name = "CONSULTA GENERAL MILITAR", NormalizedName = "CONSULTA GENERAL MILITAR", Descripcion = "Consultar solo personal militar." },
				new Role { Id = 4, Name = "CONSULTA GENERAL CIVIL", NormalizedName = "CONSULTA GENERAL CIVIL", Descripcion = "Consultar solo civiles (consultar JCE)" },
				new Role { Id = 5, Name = "CONSULTA GENERAL DEPOSITO", NormalizedName = "CONSULTA GENERAL DEPOSITO", Descripcion = "Consultar solo los depositos." },
				new Role { Id = 6, Name = "CONSULTA GENERAL FUNCION (S4)", NormalizedName = "CONSULTA GENERAL FUNCION (S4)", Descripcion = "Consultar solo las funciónes  (S4)." },
				new Role { Id = 7, Name = "GENERAR REPORTE CONSULTA", NormalizedName = "GENERAR REPORTE CONSULTA", Descripcion = "Generar las reporteria de los articulos asignados según el tipo de consulta." },
				new Role { Id = 8, Name = "MODULO MANTENIMIENTO", NormalizedName = "MODULO MANTENIMIENTO", Descripcion = "Acceso para la creacion de recursos como marcas, modelos, depositos...etc." },
				new Role { Id = 9, Name = "MODULO USUARIOS", NormalizedName = "MODULO USUARIOS", Descripcion = "Módulo para la creacion de usuarios y asignación de roles." },
				new Role { Id = 10, Name = "ASIGNACION DEPOSITO USUARIO", NormalizedName = "ASIGNACION DEPOSITO USUARIO", Descripcion = "Asignar depositos especificos para un usuario." },
				new Role { Id = 11, Name = "MODULO ESTADISTICAS", NormalizedName = "MODULO ESTADISTICAS", Descripcion = "Acceso a panel de estadisticas generales." },
				new Role { Id = 12, Name = "MODULO ALERTAS", NormalizedName = "MODULO ALERTAS", Descripcion = "Acceso al listado de las alertas." },
				new Role { Id = 13, Name = "CREAR ALERTA", NormalizedName = "CREAR ALERTA", Descripcion = "Creación de una nueva alerta para un miembro o civil." },
				new Role { Id = 14, Name = "MODULO REPORTERIA", NormalizedName = "MODULO REPORTERIA", Descripcion = "Acceso a la generación de reportes genericos." },
				new Role { Id = 15, Name = "MODULO PROVEEDORES", NormalizedName = "MODULO PROVEEDORES",Descripcion = "Acceso al modulo de proveedores y registro de licencias" }
			);
		}
	}
}
