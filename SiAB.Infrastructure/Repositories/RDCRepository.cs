using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Models.RegistroDebitoCredito;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories
{
    public class RDCRepository : IRDCRepository
	{
		private readonly AppDbContext _context;
		public RDCRepository(AppDbContext dbContext)
		{
			_context = dbContext;
		}

		public async Task<List<RegistroArticuloDebitoModel>> GetArticulosDebito(string debitadoA)
		{
			// Retrieve the data from the database
			var registros = await _context.RegistrosDebitoCredito
				.Where(r => r.DebitoA == debitadoA)
				.ToListAsync();

			if (!registros.Any()) return new List<RegistroArticuloDebitoModel>();

			// Filter the Articulos property in memory
			var articulos = registros
				.SelectMany(r => r.Articulos ?? new List<RegistroDebitoModel>())
				.ToList();			

			// Map the data to the desired model

			var mappedArticulos = articulos
				.Select(a => new RegistroArticuloDebitoModel
				{
					Id = a.Articulo.Id,
					Nombre = a.Articulo.Nombre,
					Serie = a.Serie,
					Cantidad = a.Cantidad
				})
				.ToList();

			return mappedArticulos;
		}
	}
}
