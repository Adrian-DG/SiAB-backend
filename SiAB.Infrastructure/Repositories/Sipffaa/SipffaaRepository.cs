using SiAB.Application.Contracts;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SiAB.Core.Entities.Sipffaa;

namespace SiAB.Infrastructure.Repositories
{
	public class SipffaaRepository : ISipffaaRepository
	{
		private readonly DapperContext _context;
		public SipffaaRepository(DapperContext context)
		{
			_context = context;
		}
		
		public async Task<ConsultaMiembro?> GetMiembroByCedula(string cedula)
		{
			var query = "SELECT TOP 1 * FROM Consultas.Miembros AS M WHERE M.Cedula = @cedula ORDER BY M.FechaRegistro DESC";
			
			using (var connection = _context.CreateConnection())
			{
				var result = await connection.QueryFirstOrDefaultAsync<ConsultaMiembro>(query, new { cedula });
				return result;
			}
		}
		
	}
}
