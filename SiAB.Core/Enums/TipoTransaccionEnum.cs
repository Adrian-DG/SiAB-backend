using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Enums
{
	public enum TipoTransaccionEnum
	{
		[Description("Sin una descripción clara")]
		NO_DEFINIDA = 0,
		[Description("Actualizacion de inventario, registros antiguos")]
		ACTUALIZACION_INVENTARIO = 1,
		[Description("Venta de un artículo a una persona (CIVIL).")]
		VENTA,
		[Description("Compra de un artículo a un proveedor.")]
		COMPRA,
		[Description("Transferencia de un artículo desde el inventario de la armería a una persona (militar, civil o cargo).")]
		ASIGNACION,
		[Description("Transferencia de un artículo desde una persona (militar, civil o cargo) al inventario de la armería.")]
		DEVOLUCION,
		[Description("Transferencia de un artículo entre dos personas (militar, civil o cargo).")]
		TRANSFERENCIA,
		[Description("Préstamo de un artículo a una persona (militar, civil o cargo).")]
		PRESTAMO,
		[Description("Recuperación de un artículo prestado a una persona (militar, civil o cargo).")]
		RECUPERACION,
		[Description("Retiro de un artículo del inventario de la armería.")]
		RETIRO
	}
}
