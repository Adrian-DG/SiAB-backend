using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SiAB.API.Attributes;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Data;
using System.Data;
using System.Net;

namespace SiAB.API.Controllers.Belico
{
	[ApiController]
	[AvoidCustomResponse]
	[Route("api/transacciones")]
	public class TransaccionesController : GenericController<Transaccion>
	{
		private readonly DbContextOptionsBuilder<AppDbContext> optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
		private readonly IDatabaseConnectionService _connectionService;

		public TransaccionesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, IDatabaseConnectionService databaseConnectionService) : base(unitOfWork, mapper, userContextService)
		{
			QuestPDF.Settings.License = LicenseType.Community;
			_connectionService = databaseConnectionService;
			optionsBuilder.UseSqlServer(_connectionService.GetConnectionString());
		}

		[HttpGet("filter-transacciones-serie")]
		public async Task<IActionResult> GetTransaccionesBySerie([FromQuery] string serie)
		{
			using (var context = new AppDbContext(optionsBuilder.Options))
			{
				var result = await context.SP_Obtener_Transacciones_Serie.FromSqlRaw("EXEC [Belico].[obtener_transacciones_serie] @Serie, @CodInstitucion",
						new SqlParameter("@Serie", serie),
						new SqlParameter("@CodInstitucion", _codInstitucionUsuario)).ToListAsync();

				return new JsonResult(result);

			}
		}


		[HttpGet("filter-articulos-origen-transaccion")]
		public async Task<IActionResult> GetArticulosOrigenTransaccion([FromQuery] TipoTransaccionEnum tipoOrigen, [FromQuery] string origen)
		{
			using (var context = new AppDbContext(optionsBuilder.Options))
			{
				var result = await context.SP_Obtener_Articulos_Origen_Transaccion.FromSqlRaw("EXEC [Belico].[obtener_articulos_origen_transaccion] @TipoOrigen, @Origen, @CodInstitucion",
						new SqlParameter("@TipoOrigen", (int)tipoOrigen),
						new SqlParameter("@Origen", origen.Replace("-", "")),
						new SqlParameter("@CodInstitucion", _codInstitucionUsuario)).ToListAsync();

				return new JsonResult(result);
			}
		}

		internal sealed class ArticuloItemMetadata
		{
			public int ArticuloId { get; set; }
			public int TransaccionId { get; set; }
			public int Cantidad { get; set; }
		}

		
		[HttpPost("registrar-cargo-descargo")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> CreateTransaccionCargoDescargo([FromBody] CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			var transaccion = await _uow.TransaccionRepository.CreateTransaccionCargoDescargo(transaccionCargoDescargoDto);

			return Ok(transaccion);
		}

		[HttpPost("generar-formulario-53")]
		public async Task GenerateFormulario53([FromBody] InputTransaccionReport53 InputTransaccionReport53)
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
							row.ConstantItem(80)
							.AlignMiddle()
							.Text(t =>
							{
								t.Span("Form. No.53-A\n");
								t.Span("I.G.M.B. FF.AA");
							});

							row.RelativeItem(10)
							.PaddingRight(100)
							.AlignCenter()
							.Column(col1 =>
							{
								var image = System.IO.File.ReadAllBytes(Path.Combine("Images", "Mini_Logo_Principal_MIDE.png"));

								col1.Item()
								.PaddingLeft(30)
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
					.PaddingVertical(1, Unit.Centimetre)
					.Column(column =>
					{
						column.Spacing(2);

						column.Item()
						.Row(row =>
						{
							row.Spacing(200);

							row.RelativeItem()
							.AlignLeft()
							.Text($"No. {InputTransaccionReport53.Secuencia}");

							row.RelativeItem()
							.AlignRight()
							.Text(InputTransaccionReport53.Fecha);
						});

						column.Item()
						.Border(2)
						.BorderColor(Colors.Black)
						.Text(t =>
						{
							t.Span("1.- LAS PROPIEDADES DETALLADAS A CONTINUACIÓn FUERON: Recibida al Mayor Generla, \nEN LA FECHA CITADA Y CUYA CONFORMIDAD CERTIFICO: ");
							t.Span(InputTransaccionReport53.RecibidoPor).Bold();
							t.Justify();
						});

						column.Item()	
						.Table(table =>
						{
							static IContainer CellStyle(IContainer container)
							{
								return container
								.Background(Colors.White)
								.DefaultTextStyle(x => x.FontColor(Colors.Black).Bold())
								.Border(1)
								.BorderColor(Colors.Black);
							}

							table.ColumnsDefinition(d =>
							{
								d.ConstantColumn(2, Unit.Centimetre);
								d.ConstantColumn(2, Unit.Centimetre);
								d.ConstantColumn(10, Unit.Centimetre);
								d.RelativeColumn();
							});

							table.Header(header =>
							{								
								header.Cell().Element(CellStyle).AlignCenter().Text("Cantidad");
								header.Cell().Element(CellStyle).AlignCenter().Text("Unidad");
								header.Cell().Element(CellStyle).AlignCenter().Text("Detalle");
								header.Cell().Element(CellStyle).AlignLeft().Text("Observaciones");
								
							});

							foreach (var item in InputTransaccionReport53.Articulos)
							{
								table.Cell().Element(CellStyle).AlignCenter().Text(item.Cantidad.ToString());
								table.Cell().Element(CellStyle).AlignCenter().Text("UNA");
								table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(3).Text($"{item.SubTipo} {item.Marca} Cal.0.0 {item.Serie}");
								table.Cell().Element(CellStyle).AlignLeft().Text("XXXXX");	
							}

							int template_rows_start = InputTransaccionReport53.Articulos.Count + 1;

							foreach (var item in Enumerable.Range(1, template_rows_start + 11))
							{
								if (item > template_rows_start && item < template_rows_start + 4)
								{
									table.Cell().Column(1).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(2).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(3).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(4).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
								}

								if (item == (template_rows_start + 4))
								{
									table.Cell().Column(1).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(2).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("NOTA: ").Bold();
									table.Cell().Column(3).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(4).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
								}

								else if (item > (template_rows_start + 4) && item < (template_rows_start + 11))
								{
									table.Cell().Column(1).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(2).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");

									if (item == (template_rows_start + 5))
									{
										table.Cell()
										.Column(3)
										.Row((uint)(template_rows_start + item))
										.RowSpan(8)
										.Text(t =>
										{
											t.DefaultTextStyle(st => st.FontSize(10));
											t.Span(InputTransaccionReport53.Comentarios);
										});

									}

									table.Cell().Column(4).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
								}

								else if (item == (template_rows_start + 11))
								{
									table.Cell().Column(1).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(2).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
									table.Cell().Column(3).Row((uint)(template_rows_start + item)).BorderBottom(1).BorderColor(Colors.Black).Text("");
									table.Cell().Column(4).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("");
								}

							}

						});								
						
					});

					page.Footer()
					.Column(column =>
					{
						column.Item()
						.PaddingBottom(50)
						.Row(row =>
						{
							row.Spacing(100);

							row.RelativeItem()
							.AlignLeft()
							.Text(t =>
							{
								t.Span(InputTransaccionReport53.EncargadoArmas).Bold();
								t.Span("\nMayor ERD"); // Rango encargado armas
								t.Span("\nEnc. Rec. Y Ent. de Armas IGMBFA.");
								t.AlignCenter();
							});

							row.RelativeItem()
							.AlignRight()
							.Text(t =>
							{
								t.Span(InputTransaccionReport53.EncargadoArmas).Bold();
								t.Span("\nMayor ERD"); // Rango encargado armas
								t.Span("\nEnc. Depto. Armas EF y P/C. IGMBFA.");
								t.AlignCenter();
							});
						});

						column.Item()
						.AlignCenter()
						.Row(row =>
						{
							row.AutoItem()
							.Column(col =>
							{
								col.Item()
								.LineHorizontal(1, Unit.Point)
								.LineColor(Colors.Black);

								col.Item()
								.AlignCenter()
								.Text(t =>
								{
									t.Span($"{InputTransaccionReport53.Intendente}\n");
									t.Span("Coronel, ERD. (DEM).\n");
									t.Span("Int. Gral. Material Belico de las FFAA");
								});
							});

						});

					});

				});
			}).ShowInCompanionAsync();
		}

		[HttpPost("upload-excel-relacion-articulos")]
		public async Task<IActionResult> UploadRelacionArticulos(IFormFile File, [FromQuery] InputOrigenDestinoDto inputOrigenDestinoDto)
		{
			if (File is null || File.Length == 0)
			{
				throw new BaseException("No se ha enviado un archivo", HttpStatusCode.BadRequest);
			}

			using (var stream = new MemoryStream())
			{
				await File.CopyToAsync(stream);

				using (ExcelPackage excel = new ExcelPackage(stream))
				{
					var workBook = excel.Workbook;

					foreach (var worksheet in workBook.Worksheets)
					{
						var articulosData = new List<ArticuloItemMetadata>();

						int start_row = 2;
						int row_dimmession = worksheet.Dimension.End.Row;

						for (int row = start_row; row <= row_dimmession; row++)
						{
							var origen = worksheet.Cells[row, 1].Value.ToString(); // Cedula o Deposito
							var destino  = worksheet.Cells[row, 2].Value?.ToString(); // Cedula o Deposito
							var categoria = worksheet.Cells[row, 3].Value?.ToString();
							var tipo = worksheet.Cells[row, 4].Value?.ToString();
							var subtipo = worksheet.Cells[row, 5].Value?.ToString();
							var marca = worksheet.Cells[row, 6].Value?.ToString();
							var modelo = worksheet.Cells[row, 7].Value?.ToString();
							var calibre = worksheet.Cells[row, 8].Value?.ToString();
							var serie = worksheet.Cells[row, 9].Value?.ToString();
							var formulario53 = worksheet.Cells[row, 10].Value?.ToString();
							var fechaValue = worksheet.Cells[row, 11].Value?.ToString();

							DateTime fechaEfectividad = int.TryParse(fechaValue, out int result) 
								? DateTime.FromOADate(Convert.ToDouble(fechaValue)) 
								: DateTime.Parse(fechaValue); 
												
							var articuloId = await InsertArticulo(
									categoria: categoria,
									tipo: tipo,
									subtipo: subtipo,
									marca: marca,
									modelo: modelo,
									calibre: calibre,
									serie: serie);

							var transaccionId = await InsertTransaccion(
								tipoOrigen: inputOrigenDestinoDto.Origen,
								origen: origen,
								tipoDestino: inputOrigenDestinoDto.Destino,
								destino: destino.ToString().Replace("-", ""),
								fechaEfectividad: fechaEfectividad);

							await InsertDocumentoTransaccion(numero: formulario53, transaccionId: transaccionId);

							articulosData.Add(new ArticuloItemMetadata { ArticuloId = articuloId, TransaccionId = transaccionId, Cantidad = 1 });
						
						}

						await InsertDetalleTransaccion(articulosData);

						
					}
				}
			}

			return Ok();
		}

		private async Task InsertDocumentoTransaccion(string numero, int transaccionId)
		{
			using (var context = new AppDbContext(optionsBuilder.Options))
			{
				await context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
				{
					NumeracionDocumento = $"F-53-{numero}",
					TransaccionId = transaccionId,
					Archivo = null,
					UsuarioId = _codUsuario,
					CodInstitucion = (InstitucionEnum)_codInstitucionUsuario
				});

				await context.SaveChangesAsync();
			}
		}


		private async Task<int> InsertArticulo(string categoria, string tipo, string subtipo, string marca, string modelo, string calibre, string serie)
		{
			using (var context = new AppDbContext(optionsBuilder.Options))
			{
				// Define output parameter
				var outputParamter = new SqlParameter("@ArticuloId", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

				// Prepare other parameters
				var parameters = new List<SqlParameter>
				{
					new SqlParameter("@Categoria", categoria),
					new SqlParameter("@Tipo", tipo),
					new SqlParameter("@SubTipo", subtipo),
					new SqlParameter("@Marca", marca),
					new SqlParameter("@Modelo", modelo),
					new SqlParameter("@Calibre", calibre),
					new SqlParameter("@Serie", serie),
					new SqlParameter("@CodUsuario", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario),
					outputParamter
				};

				// Execute stored procedure
				await context.Database.ExecuteSqlRawAsync("EXEC [Inv].[Agregar_Articulo_Inventario] @Categoria, @Tipo, @SubTipo, @Marca, @Modelo, @Calibre, @Serie, @CodUsuario, @CodInstitucion, @ArticuloId OUTPUT", parameters.ToArray());

				return (int)outputParamter.Value;
			}
		}

		private async Task<int> InsertTransaccion(TipoTransaccionEnum tipoOrigen, string origen, TipoTransaccionEnum tipoDestino, string destino, DateTime fechaEfectividad)
		{
			using (var context = new AppDbContext(optionsBuilder.Options))
			{
				// Prepare parameters
				var outputParameter = new SqlParameter("@TransaccionId", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

				var parameters = new SqlParameter[]
				{
					new SqlParameter("@TipoOrigen", (int)tipoOrigen),
					new SqlParameter("@Origen", origen),
					new SqlParameter("@TipoDestino", (int)tipoDestino),
					new SqlParameter("@Destino", destino),
					new SqlParameter("@FechaEfectividad", fechaEfectividad.ToString("yyyy-MM-dd")),
					new SqlParameter("@CodUsuario", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario),
					outputParameter
				};

				await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_transaccion] @TipoOrigen, @Origen, @TipoDestino, @Destino, @FechaEfectividad, @CodUsuario, @CodInstitucion, @TransaccionId OUTPUT", parameters);

				return (int)outputParameter.Value;
			}
		}

		
		private async Task InsertDetalleTransaccion(List<ArticuloItemMetadata> data)
		{
			foreach (var item in data)
			{
				var parameters = new SqlParameter[]
				{
					new SqlParameter("@ArticuloId", item.ArticuloId),
					new SqlParameter("@TransaccionId", item.TransaccionId),
					new SqlParameter("@UsuarioId", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario)
				};

				using (var context = new AppDbContext(optionsBuilder.Options))
				{
					await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_detalle_transaccion] @ArticuloId, @TransaccionId, @UsuarioId, @CodInstitucion", parameters);
				}
			}
			
		}


	}
}

