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
				new Role { Id = 1, Name = "ADMINISTRADOR GENERAL", NormalizedName = "ADMINISTRADOR", Descripcion = "Acceso global a todos los módulos del sistema." },
				new Role { Id = 2, Name = "MÓDULO CONSULTA GENERAL", NormalizedName = "MODULO_CONSULTA_GENERAL", Descripcion = "Modulo para consulta de existencia por miembro, civil, deposito y función(S4)." },
				new Role { Id = 3, Name = "CONSULTA GENERAL MILITAR", NormalizedName = "CONSULTA_GENERAL_MILITAR", Descripcion = "Consultar solo personal militar." },
				new Role { Id = 4, Name = "CONSULTA GENERAL CIVIL", NormalizedName = "CONSULTA_GENERAL_CIVIL", Descripcion = "Consultar solo civiles (consultar JCE)" },
				new Role { Id = 5, Name = "CONSULTA GENERAL DEPOSITO", NormalizedName = "CONSULTA_GENERAL_DEPOSITO", Descripcion = "Consultar solo los depositos." },
				new Role { Id = 6, Name = "CONSULTA GENERAL FUNCIÓN (S4)", NormalizedName = "CONSULTA_GENERAL_FUNCION", Descripcion = "Consultar solo las funciónes  (S4)." },
				new Role { Id = 7, Name = "GENERAR REPORTE CONSULTA", NormalizedName = "REPORTE_CONSULTA", Descripcion = "Generar las reporteria de los articulos asignados según el tipo de consulta." },
				new Role { Id = 8, Name = "MÓDULO MANTENIMIENTO", NormalizedName = "MANTENIMIENTO", Descripcion = "Acceso para la creacion de recursos como marcas, modelos, depositos...etc." },
				new Role { Id = 9, Name = "MÓDULO USUARIOS", NormalizedName = "USUARIOS", Descripcion = "Módulo para la creacion de usuarios y asignación de roles." },
				new Role { Id = 10, Name = "ASIGNACIÓN DEPOSITO-USUARIO", NormalizedName = "DEPOSITO-USUARIO", Descripcion = "Asignar depositos especificos para un usuario." },
				new Role { Id = 11, Name = "MÓDULO ESTADISTICAS", NormalizedName = "ESTADISTICAS", Descripcion = "Acceso a panel de estadisticas generales." },
				new Role { Id = 12, Name = "MÓDULO ALERTAS", NormalizedName = "LISTADO_ALERTAS", Descripcion = "Acceso al listado de las alertas." },
				new Role { Id = 13, Name = "CREAR ALERTA", NormalizedName = "CREAR_ALERTA", Descripcion = "Creación de una nueva alerta para un miembro o civil." },
				new Role { Id = 14, Name = "MÓDULO REPORTERIA", NormalizedName = "REPORTERIA", Descripcion = "Acceso a la generación de reportes genericos." }
			);
		}
	}
}
