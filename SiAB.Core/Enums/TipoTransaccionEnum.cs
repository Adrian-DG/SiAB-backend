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
		[Description("Venta de un artículo a una persona (CIVIL).")]
		VENTA = 1,
		[Description("Compra de un artículo a un proveedor.")]
		COMPRA = 2,
		[Description("Transferencia de un artículo desde el inventario de la armería a una persona (militar, civil o cargo).")]
		ASIGNACION = 3,
		[Description("Transferencia de un artículo desde una persona (militar, civil o cargo) al inventario de la armería.")]
		DEVOLUCION = 4,
		[Description("Transferencia de un artículo entre dos personas (militar, civil o cargo).")]
		TRANSFERENCIA = 5,
		[Description("Préstamo de un artículo a una persona (militar, civil o cargo).")]
		PRESTAMO = 6,
		[Description("Recuperación de un artículo prestado a una persona (militar, civil o cargo).")]
		RECUPERACION = 7,
		[Description("Retiro de un artículo del inventario de la armería.")]
		RETIRO
	}
}
