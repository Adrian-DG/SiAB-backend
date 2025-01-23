using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Entities.Sipffaa;
using SiAB.Core.Models.Sipffaa;

namespace SiAB.Application.Contracts
{
	public interface ISipffaaRepository
	{
		Task<IEnumerable<MiembroListDetail>> GetMiembrosByCedulaNombre(string param);
		Task<ConsultaMiembro?> GetMiembroByCedula(string cedula);
	}
}
