using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface ISecuenciaRepository
	{
		Task<string> GetSecuenciaInstitucion(int CodInstitucion);
		Task CreateSecuencia(int CodInstitucion);
	}
}
