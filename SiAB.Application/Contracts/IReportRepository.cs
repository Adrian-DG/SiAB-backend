using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Sipffaa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IReportRepository
	{
		Task<byte[]> GetHistorialArmasCargadas(ConsultaMiembro miembro);
	}
}
