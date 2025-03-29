﻿using Microsoft.EntityFrameworkCore;
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
				new Role { Id = 1, Name = "ADMINISTRADOR GENERAL", NormalizedName = "ADMINISTRADOR GENERAL", Descripcion = "ACCESO ILIMITADO A TODAS LAS FUNCIONES DEL SISTEMA" },
				new Role { Id = 2, Name = "MODULO EXISTENCIAS", NormalizedName = "MODULO EXISTENCIAS", Descripcion = "ACCESO AL MODULO DE EXISTENCIAS" },

				new Role { Id = 3, Name = "MODULO TRANSACCIONES", NormalizedName = "MODULO TRANSACCIONES", Descripcion = "ACCESO AL MODULO DE TRANSACCIONES" },
				new Role { Id = 4, Name = "TRANSACCIONES CREAR CARGO DESCARGO", NormalizedName = "TRANSACCIONES CREAR CARGO DESCARGO", Descripcion = "CREAR CARGOS Y DESCARGOS" },
				new Role { Id = 5, Name = "TRANSACCIONES VISUALIZAR DETALLES", NormalizedName = "TRANSACCIONES VISUALIZAR DETALLES", Descripcion = "VISUALIZAR DETALLES DE LAS TRANSACCIONES" },
				new Role { Id = 6, Name = "TRANSACCIONES VISUALIZAR DOCUMENTOS", NormalizedName = "TRANSACCIONES VISUALIZAR DOCUMENTOS", Descripcion = "VISUALIZAR DOCUMENTOS DE LAS TRANSACCIONES" },
				new Role { Id = 7, Name = "TRANSACCIONES GENERAR FORMULARIO 53", NormalizedName = "TRANSACCIONES GENERAR FORMULARIO 53", Descripcion = "GENERAR FORMULARIO 53" },
				new Role { Id = 8, Name = "TRANSACCIONES ADJUNTAR FORMULARIO 53", NormalizedName = "TRANSACCIONES ADJUNTAR FORMULARIO 53", Descripcion = "ADJUNTAR FORMULARIO 53" },
				new Role { Id = 9, Name = "TRANSACCIONES CARGAR INVENTARIO EXCEL", NormalizedName = "TRANSACCIONES CARGAR INVENTARIO EXCEL", Descripcion = "CARGAR INVENTARIO DESDE EXCEL" },

				new Role { Id = 10, Name = "MODULO MANTENIMIENTO", NormalizedName = "MODULO MANTENIMIENTO", Descripcion = "ACCESO AL MODULO DE MANTENIMIENTO" },
				new Role { Id = 11, Name = "MODULO EMPRESAS", NormalizedName = "MODULO EMPRESAS", Descripcion = "ACCESO AL MODULO DE EMPRESAS" },
				new Role { Id = 12, Name = "EMPRESAS CREAR", NormalizedName = "EMPRESAS CREAR", Descripcion = "CREAR EMPRESAS" },
				new Role { Id = 13, Name = "EMPRESAS EDITAR", NormalizedName = "EMPRESAS EDITAR", Descripcion = "EDITAR EMPRESAS" },
				new Role { Id = 14, Name = "EMPRESAS ELIMINAR", NormalizedName = "EMPRESAS ELIMINAR", Descripcion = "ELIMINAR EMPRESAS" },

				new Role { Id = 15, Name = "EMPRESAS VISUALIZAR ORDENES", NormalizedName = "EMPRESAS VISUALIZAR ORDENES", Descripcion = "VISUALIZAR ORDENES DE EMPRESAS" },
				new Role { Id = 16, Name = "EMPRESAS CREAR ORDEN", NormalizedName = "EMPRESAS CREAR ORDEN", Descripcion = "CREAR ORDENES DE EMPRESAS" },
				new Role { Id = 17, Name = "EMPRESAS EDITAR ORDEN", NormalizedName = "EMPRESAS EDITAR ORDEN", Descripcion = "EDITAR ORDENES DE EMPRESAS" },
				new Role { Id = 18, Name = "EMPRESAS ELIMINAR ORDEN", NormalizedName = "EMPRESAS ELIMINAR ORDEN", Descripcion = "ELIMINAR ORDENES DE EMPRESAS" },
				new Role { Id = 19, Name = "EMPRESAS VISUALIZAR DETALLES ORDEN", NormalizedName = "EMPRESAS VISUALIZAR DETALLES ORDEN", Descripcion = "VISUALIZAR DETALLES DE ORDENES DE EMPRESAS" },
				new Role { Id = 20, Name = "EMPRESAS VISUALIZAR DOCUMENTOS ORDEN", NormalizedName = "EMPRESAS VISUALIZAR DOCUMENTOS ORDEN", Descripcion = "VISUALIZAR DOCUMENTOS DE ORDENES DE EMPRESAS" },
				
				new Role { Id = 21, Name = "MODULO USUARIOS", NormalizedName = "MODULO USUARIOS", Descripcion = "ACCESO AL MODULO DE USUARIOS" }
			);
		}
	}
}
