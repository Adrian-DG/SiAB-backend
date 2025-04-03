using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class SecuenciaRepository : ISecuenciaRepository
	{
		private readonly AppDbContext _context;
		public SecuenciaRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task CreateSecuencia(int CodInstitucion)
		{
			// find the last sequence
			var lastSequence = await _context.Secuencias.Where(s => s.CodInstitucion == (InstitucionEnum)CodInstitucion && s.FechaCreacion.Year == DateTime.Now.Year)
				.OrderByDescending(s => s.SecuenciaNumero)
				.FirstOrDefaultAsync();

			// if there is no sequence, create the first one
			if (lastSequence != null)
			{
				await _context.Secuencias.AddAsync(new Core.Entities.Belico.Secuencia
				{
					SecuenciaCadena = GenerarSecuencia(lastSequence.SecuenciaNumero + 1, CodInstitucion),
					SecuenciaNumero = lastSequence.SecuenciaNumero + 1,
					CodInstitucion = (InstitucionEnum)CodInstitucion
				});
			}
			else
			{
				await _context.Secuencias.AddAsync(new Core.Entities.Belico.Secuencia
				{
					SecuenciaCadena = GenerarSecuencia(1, CodInstitucion),
					SecuenciaNumero = 1,
					CodInstitucion = (InstitucionEnum)CodInstitucion
				});
			}

			await _context.SaveChangesAsync();
		}

		public async Task<string> GetSecuenciaInstitucion(int CodInstitucion)
		{
			// find the last sequence
			var lastSequence = await _context.Secuencias.Where(s => s.CodInstitucion == (InstitucionEnum)CodInstitucion && s.FechaCreacion.Year == DateTime.Now.Year)
				.OrderByDescending(s => s.SecuenciaNumero)
				.FirstOrDefaultAsync();

			return lastSequence is null 
				? GenerarSecuencia(1, CodInstitucion) 
				: GenerarSecuencia(lastSequence.SecuenciaNumero + 1, CodInstitucion);
		}

		private string GenerarSecuencia(int numero, int CodInstitucion)
		{
			var fecha_cadena = DateTime.Now.ToString("yyyy-MM-dd");
			var numero_cadena = "" + (numero < 10 ? "0" + numero : numero);

			return ((InstitucionEnum)CodInstitucion) switch
			{
				InstitucionEnum.MIDE => $"{numero_cadena}-{DateTime.Now.Year}",
				InstitucionEnum.ERD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				InstitucionEnum.ARD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				InstitucionEnum.FARD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				_ => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest)
			};
		}
	}
}
