using SiAB.Application.Contracts;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SiAB.Core.Entities.Sipffaa;
using SiAB.Core.Models.Sipffaa;
using SiAB.Core.Exceptions;

namespace SiAB.Infrastructure.Repositories
{
	public class SipffaaRepository : ISipffaaRepository
	{
		private readonly DapperContext _context;
		public SipffaaRepository(DapperContext context)
		{
			_context = context;
		}

		private async Task<byte[]?> GetMiembroFoto(string cedula)
		{
			var query = $@"SELECT TOP 1 M.foto AS Foto
						   FROM miembros.MIEMBROS AS M
						   WHERE M.Cedula = @cedula
						   ORDER BY M.FechaRegistro DESC";

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryFirstOrDefaultAsync<byte[]?>(query, new { cedula });
				return result ?? null;
			}

		}

		public async Task<IEnumerable<MiembroListDetail>> GetMiembrosByCedula(string cedula)
		{
			var query = @$"SELECT TOP 10 
						  M.Rango,
						  M.Cedula,
						  M.NombreApellidoCompleto,
						  M.EstadoMiembro
						  FROM Consultas.Miembros AS M
						  WHERE M.Cedula LIKE '%{cedula}%' ORDER BY M.CodRango ASC";

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryAsync<MiembroListDetail>(query);
				return result;
			}
		}

		public async Task<ConsultaMiembro?> GetMiembroByCedula(string cedula)
		{
			var query = "SELECT TOP 1 * FROM Consultas.Miembros AS M WHERE M.Cedula = @cedula ORDER BY M.FechaRegistro DESC";

			byte[]? foto = await GetMiembroFoto(cedula);

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryFirstOrDefaultAsync<ConsultaMiembro>(query, new { cedula });

				if (result is null)
				{
					throw new BaseException("Miembro no encontrado", System.Net.HttpStatusCode.NotFound);
				}

				result.Foto = (foto is null) ? null : Convert.ToBase64String(foto);
				return result;
			}
		}
		
	}
}
