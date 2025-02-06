using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Infrastructure.Data;
using System;
using System.Reflection.Metadata;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
	{
		private readonly AppDbContext _context;
		private const int LICENCIA_DOCUMENTO_ID = 3;
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

			foreach (var item in createEmpresaDto.DataArchivos)
			{
				await _context.Licencias.AddAsync(new LicenciaEmpresa
				{
					Numeracion = item.Numeracion,
					FechaEmision = DateOnly.FromDateTime(item.FechaEmision),
					FechaVigencia = DateOnly.FromDateTime(item.FechaVigencia),
					FechaVencimiento = DateOnly.FromDateTime(item.FechaVencimiento),
					EmpresaId = proveedor.Entity.Id,
					TipoDocumentoId = item.TipoDocumentoId,
				});

				await _context.SaveChangesAsync();
			}
		}


	}
}
