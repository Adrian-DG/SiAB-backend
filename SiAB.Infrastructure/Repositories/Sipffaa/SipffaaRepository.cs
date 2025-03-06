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
using System.Runtime.ConstrainedExecution;
using System.Net;
using System.Drawing;

namespace SiAB.Infrastructure.Repositories
{
    public class SipffaaRepository : ISipffaaRepository
	{
		private readonly DapperContext _context;
		public SipffaaRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<byte[]?> GetMiembroFoto(string cedula)
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

		public async Task<IEnumerable<MiembroListDetail>> GetMiembrosByCedulaNombre(string param)
		{
			var query = @$"SELECT TOP 10 
					      M2.foto as Foto,
						  M.Rango,
						  M.Cedula,
						  M.NombreApellidoCompleto,
						  M.EstadoMiembro
						  FROM Consultas.Miembros AS M
						  LEFT JOIN miembros.MIEMBROS AS M2 ON M2.CodMiembro = M.CodMiembro AND M2.CodInstitucion = M.CodInstitucion
						  WHERE M.Cedula LIKE '%{param}%' OR M.NombreApellidoCompleto LIKE '%{param}%' ORDER BY M.CodRango ASC";

			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryAsync<MiembroListQueryResult>(query);

				var list = result.Select(x => new MiembroListDetail
				{
					Foto = (x.Foto is null) ? null : Convert.ToBase64String(x.Foto),
					Rango = x.Rango,
					Cedula = x.Cedula,
					NombreApellidoCompleto = x.NombreApellidoCompleto,
					EstadoMiembro = x.EstadoMiembro
				}).ToList();

				return list;
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
