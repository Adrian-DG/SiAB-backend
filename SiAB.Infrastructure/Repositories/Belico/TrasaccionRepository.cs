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
						TipoOrigen = transaccionCargoDescargoDto.TipoCargoDebito,
						Origen = transaccionCargoDescargoDto.Debito,
						TipoDestino = transaccionCargoDescargoDto.TipoCargoCredito,
						Destino = transaccionCargoDescargoDto.Credito,
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

		public async Task GenerateFormulario53(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			await Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(1, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(12));

					// Header 

					page.Header()
					.Column(column =>
					{
						column.Item()
						.AlignLeft()
						.Row(row =>
						{
							row.ConstantItem(100)
							.Text($"Form. No.53A");

							row.ConstantItem(800)
							.AlignCenter()
							.Column(col1 =>
							{
								var image = System.IO.File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Images", "Mini_Logo_Principal_MIDE.png"));

								col1.Item()
								.AlignCenter()
								.Width(150)
								.Height(100)
								.Image(image);

								col1.Item()
								.AlignCenter()
								.Text("REPÚBLICA DOMINICANA");

								col1.Item()
								.AlignCenter()
								.Text("INTENDECIA GENERAL DEL MATERIAL BELICO, FF.AA.");

								col1.Item()
								.AlignCenter()
								.Text("DIRECCION GENERAL DEL MATERIAL BELICO");

							});

						});

					});

					// Content 

					page.Content()
					.PaddingVertical(2, Unit.Centimetre)
					.Column(column =>
					{
						column.Item()
						.Row(row =>
						{
							row.ConstantItem(100)
							.AlignLeft()
							.Text($"No. {transaccionCargoDescargoDto.Secuencia}");

							row.ConstantItem(100)
							.AlignRight()
							.Text(transaccionCargoDescargoDto.Fecha);
						});

						column.Item()
						.Border(2)
						.BorderColor(Colors.Black)
						.Text(t =>
						{
							t.Span("1.- LAS PROPIEDADES DETALLADAS A CONTINUACIÓn FUERON: Recibida al Mayor Generla, \nEN LA FECHA CITADA Y CUYA CONFORMIDAD CERTIFICO: ");
							t.Span("(NOMBRE_QUIEN_CERTIFICA").Bold();
						});

					});

				});
			}).ShowInCompanionAsync();
		}
	}
}
