using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
	{
		private readonly AppDbContext _context;
		public ProveedorRepository(AppDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		public async Task CreateProveedor_Licencia(CreateProveedorDto createProveedorDto)
		{
			var proveedor = await _context.Proveedores.AddAsync(new Proveedor
			{
				Nombre = createProveedorDto.Nombre,
				Telefono = createProveedorDto.Telefono,
				RNC = createProveedorDto.RNC
			});

			await _context.SaveChangesAsync();

			await _context.Licencias.AddAsync(new LicenciaProveedor
			{
				Numeracion = createProveedorDto.Numeracion,
				TipoLicencia = createProveedorDto.TipoLicencia,
				Archivo = createProveedorDto.Archivo,
				ProveedorId = proveedor.Entity.Id,
				FechaEmision = DateOnly.FromDateTime(createProveedorDto.FechaEmision),
				FechaVencimiento = DateOnly.FromDateTime(createProveedorDto.FechaVencimiento),
			});

			await _context.SaveChangesAsync();

		}

	}
}
