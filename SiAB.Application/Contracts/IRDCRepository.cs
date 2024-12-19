using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Models.RegistroDebitoCredito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
    public interface IRDCRepository 
	{
		Task<List<RegistroArticuloDebitoModel>> GetArticulosDebito(string debitadoA);
	}
}
