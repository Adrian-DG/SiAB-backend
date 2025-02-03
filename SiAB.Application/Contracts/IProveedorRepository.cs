using SiAB.Core.DTO.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IProveedorRepository
	{
		Task CreateProveedor_Licencia(CreateProveedorDto createProveedorDto);
	}
}
