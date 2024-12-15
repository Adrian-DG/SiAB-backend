using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiAB.Core.Entities.Sipffaa;

namespace SiAB.Application.Contracts
{
	public interface ISipffaaRepository
	{
		Task<Miembro?> GetMiembroByCedula(string cedula);
	}
}
