using SiAB.Core.DTO.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface ITransaccionRepository
	{
		Task<int> CreateTransaccionCargoDescargo(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto);
		Task GenerateFormulario53(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto);

		Task SaveFormulario53(int transaccionId, string archivo);
	}
}
