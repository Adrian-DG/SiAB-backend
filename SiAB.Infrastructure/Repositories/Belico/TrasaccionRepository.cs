using Microsoft.AspNetCore.Http;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class TrasaccionRepository : ITransaccionRepository
	{
		private readonly AppDbContext _context;
		public TrasaccionRepository(AppDbContext context)
		{
			QuestPDF.Settings.License = LicenseType.Community;
			_context = context;
		}

		public async Task<int> CreateTransaccionCargoDescargo(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{

					var transaccion = await _context.Transacciones.AddAsync(new Transaccion
					{
						TipoOrigen = transaccionCargoDescargoDto.TipoCargoCredito,
						Origen = transaccionCargoDescargoDto.Credito,
						TipoDestino = transaccionCargoDescargoDto.TipoCargoDebito,
						Destino = transaccionCargoDescargoDto.Debito,
						FechaEfectividad = DateOnly.Parse(transaccionCargoDescargoDto.Fecha),

						Intendente = transaccionCargoDescargoDto.Intendente,
						EncargadoGeneral = transaccionCargoDescargoDto.EncargadoArmas,
						EncargadoDeposito = transaccionCargoDescargoDto.EncargadoDepositos,

						EntregadoA = transaccionCargoDescargoDto.Entrega,
						RecibidoPor = transaccionCargoDescargoDto.Recibe,
						FirmadoPor = transaccionCargoDescargoDto.Firma,

					});

					await _context.SaveChangesAsync();

					var archivoBase64 = DateUrlToBase64Converter.ConvertDataUrlToBase64String(transaccionCargoDescargoDto.Documento);
					var archivoByte = Convert.FromBase64String(archivoBase64);

					await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
					{
						TransaccionId = transaccion.Entity.Id,
						TipoDocuemntoId = 1,
						NumeracionDocumento = transaccionCargoDescargoDto.NoDocumento,
						Archivo = archivoByte,						
					});

					await _context.SaveChangesAsync();

					foreach (var item in transaccionCargoDescargoDto.Articulos)
					{
						await _context.DetallesArticuloTransaccion.AddAsync(new DetalleArticuloTransaccion
						{
							ArticuloId = item.Id,
							Cantidad = item.Cantidad,
							TransaccionId = transaccion.Entity.Id
						});

						await _context.SaveChangesAsync();
					}

					await transaction.CommitAsync();

					return transaccion.Entity.Id;

				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw;
				}
			}			
		}

		public async Task SaveFormulario53(int transaccionId, string archivo)
		{
			var archivoBase64 = DateUrlToBase64Converter.ConvertDataUrlToBase64String(archivo);
			var archivoByte = Convert.FromBase64String(archivoBase64);

			await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
			{
				TransaccionId = transaccionId,
				TipoDocuemntoId = 1,
				NumeracionDocumento = $"formulario_53_{transaccionId}",
				Archivo = archivoByte,
			});

			await _context.SaveChangesAsync();
		}

		public Task GenerateFormulario53(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			throw new NotImplementedException();
		}
	}
}
