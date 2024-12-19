using SiAB.Core.Models.JCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
    public interface IJCERepository
	{
		JCEResult GetInfoCivilByCedula(string cedula);
	}
}
