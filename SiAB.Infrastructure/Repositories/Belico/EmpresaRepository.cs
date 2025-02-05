using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Infrastructure.Data;
using System;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
	{
		private readonly AppDbContext _context;
		public EmpresaRepository(AppDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		public async Task CreateEmpresa_Licencia(CreateEmpresaDto createEmpresaDto)
		{
			var proveedor = await _context.Empresas.AddAsync(new Empresa
			{
				Nombre = createEmpresaDto.Nombre,
				Telefono = createEmpresaDto.Telefono,
				RNC = createEmpresaDto.RNC,
				Titular = createEmpresaDto.Titular,
			});

			await _context.SaveChangesAsync();

			await _context.Licencias.AddAsync(new LicenciaEmpresa
			{
				Numeracion = createEmpresaDto.Numeracion,
				TipoDocumentoId = createEmpresaDto.TipoDocumentoId,
				Archivo = createEmpresaDto.Archivo,
				EmpresaId = proveedor.Entity.Id,
				FechaEmision = createEmpresaDto.FechaEmision,
				FechaVencimiento = createEmpresaDto.FechaVencimiento,
			});

			await _context.SaveChangesAsync();

		}

	}
}
