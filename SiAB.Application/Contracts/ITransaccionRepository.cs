using Microsoft.AspNetCore.Http;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using SiAB.Core.Models.Transacciones;
using SiAB.Core.Models;
using SiAB.Core.ProcedureResults;
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

		Task<PagedData<TransaccionViewModel>> GetTransacciones(TransaccionPaginationFilter filters, int CodInstitucion, string[] roles);

		Task<object> GetTransaccionDetails(int Id);

		byte[] GenerateFormulario53(InputTransaccionReport53 InputTransaccionReport53);

		Task SaveDocumento(AdjuntarFormularioTransaccionDto adjuntarFormulario53Dto);

		Task<List<SerieTransaccionItem>> GetTransaccionesBySerie(string serie);

		Task<List<ArticuloTransaccionItem>> GetArticulosOrigenTransaccion(TipoTransaccionEnum tipoOrigen, string origen);

		Task UploadRelacionArticulos(IFormFile File, InputOrigenDestinoDto inputOrigenDestinoDto);
	}
}
