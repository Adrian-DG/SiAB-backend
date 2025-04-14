using SiAB.Core.DTO.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Application.Contracts
{
	public interface IEmpresaRepository 
	{
		Task CreateEmpresa(CreateEmpresaDto createEmpresaDto);
		Task CreateOrdenEmpresa(int Id, CreateOrdenEmpresaDto createOrdenEmpresaDto);
		Task<object> GetDetalleOrdenEmpresa(int OrdenId);
	}
}
