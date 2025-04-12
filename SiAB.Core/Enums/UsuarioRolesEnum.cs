﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Enums
{
	public enum UsuarioRolesEnum
	{
		ADMINISTRADOR_GENERAL = 1,
		MODULO_EXISTENCIAS = 2,
		MODULO_TRANSACCIONES = 3,
		TRANSACCIONES_CREAR_CARGO_DESCARGO = 4,
		TRANSACCIONES_VISUALIZAR_DETALLES = 5,
		TRANSACCIONES_VISUALIZAR_DOCUMENTOS = 6,
		TRANSACCIONES_GENERAR_FORMULARIO_53 = 7,
		TRANSACCIONES_ADJUNTAR_FORMULARIO_53 = 8,
		TRANSACCIONES_CARGAR_INVENTARIO_EXCEL = 9,

		MODULO_MANTENIMIENTOS = 10,
		MANTENIMIENTO_INSTITUCIONES = 11,
		MANTENIMIENTO_DEPENDENCIAS = 12,
		MANTENIMIENTO_PROPIEDADES = 13,
		MANTENIMIENTO_ARTICULOS = 14,
		MANTENIMIENTO_TIPOS = 15,
		MANTENIMIENTO_SUBTIPOS = 16,
		MANTENIMIENTO_TIPOS_DOCUMENTOS = 17,
		MANTENIMIENTO_CALIBRES = 18,
		MANTENIMIENTO_MARCAS = 19,
		MANTENIMIENTO_MODELOS = 20,
		MANTENIMIENTO_CATEGORIAS = 21,

		MODULO_EMPRESAS = 22,
		EMPRESAS_CREAR = 23,
		EMPRESAS_EDITAR = 24,
		EMPRESAS_ELIMINAR = 25,
		EMPRESAS_VISUALIZAR_ORDENES = 26,
		EMPRESAS_CREAR_ORDEN = 27,
		EMPRESAS_EDITAR_ORDEN = 28,
		EMPRESAS_ELIMINAR_ORDEN = 29,
		EMPRESAS_VISUALIZAR_DETALLES_ORDEN = 30,
		EMPRESAS_VISUALIZAR_DOCUMENTOS_ORDEN = 31,
		MODULO_USUARIOS = 32
	}
}


