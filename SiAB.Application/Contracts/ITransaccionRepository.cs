using Microsoft.AspNetCore.Http;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Enums;
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

		byte[] GenerateFormulario53(InputTransaccionReport53 InputTransaccionReport53);

		Task SaveFormulario53(int transaccionId, string archivo);

		Task<List<SerieTransaccionItem>> GetTransaccionesBySerie(string serie);

		Task<List<ArticuloTransaccionItem>> GetArticulosOrigenTransaccion(TipoTransaccionEnum tipoOrigen, string origen);

		Task UploadRelacionArticulos(IFormFile File, InputOrigenDestinoDto inputOrigenDestinoDto);
	}
}
