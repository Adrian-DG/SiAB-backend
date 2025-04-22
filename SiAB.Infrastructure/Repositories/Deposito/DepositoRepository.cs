using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Enums;
using SiAB.Core.Models;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Deposito
{
	public class DepositoRepository : IDepositoRepository
	{
		private readonly AppDbContext _context;
		public DepositoRepository(AppDbContext appContext)
		{
			_context = appContext;
		}

		public async Task<List<GroupEntityModel>> GetStockByInventario()
		{
			var result = await _context.StockArticulos
				.Where(s => s.TipoEntidad == TipoOrigenDestinoEnum.DEPOSITO)
				.GroupBy(s => s.Entidad)
				.Select(g => new GroupEntityModel
				{
					Nombre = g.Key,
					Total = g.Sum(s => s.Cantidad)
				})
				.ToListAsync();

			return result;
		}

	}
}
