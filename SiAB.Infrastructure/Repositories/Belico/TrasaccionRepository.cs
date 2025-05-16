using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Core.Extensions;
using SiAB.Core.Models;
using SiAB.Core.Models.Transacciones;
using SiAB.Core.ProcedureResults;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Belico
{
	public class TrasaccionRepository : ITransaccionRepository
	{
		private Dictionary<string, int> datosArticulosDict;
		private readonly AppDbContext _context;

		public TrasaccionRepository(AppDbContext dbContext) 
		{
			QuestPDF.Settings.License = LicenseType.Community;			
			datosArticulosDict = new Dictionary<string, int>();
			_context = dbContext;
		}

		public async Task<int> CreateTransaccionCargoDescargo(CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
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
						Estatus = EstatusTransaccionEnum.EN_PROCESO,
					});

					await _context.SaveChangesAsync();

					var archivoBase64 = DataUrlToBase64Converter.ConvertDataUrlToBase64String(transaccionCargoDescargoDto.Documento);
					var archivoByte = Convert.FromBase64String(archivoBase64);

					await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
					{
						TransaccionId = transaccion.Entity.Id,
						TipoDocumentoId = (int)TipoDocumentoEnum.NO_DEFINIDO,
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

		public async Task SaveDocumento(AdjuntarFormularioTransaccionDto adjuntarFormulario53Dto)
		{
			if (adjuntarFormulario53Dto.Url is null || adjuntarFormulario53Dto.Numeracion is null)
			{
				throw new BaseException("No se ha enviado un archivo o falta la numeracion", HttpStatusCode.BadRequest);
			}

			var archivoBase64 = DataUrlToBase64Converter.ConvertDataUrlToBase64String(adjuntarFormulario53Dto.Url);
			var archivoByte = Convert.FromBase64String(archivoBase64);

			if (adjuntarFormulario53Dto.TipoDocumentoId == (int)TipoDocumentoEnum.FORMULARIO_53)
			{
				var formulario53 = await _context.DocumentosTransaccion.FirstOrDefaultAsync(dt => 
				dt.TransaccionId == adjuntarFormulario53Dto.Id 
				&& dt.TipoDocumentoId == (int)TipoDocumentoEnum.FORMULARIO_53);

				if (formulario53 is null)
				{
					await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
					{
						TransaccionId = adjuntarFormulario53Dto.Id,
						TipoDocumentoId = adjuntarFormulario53Dto.TipoDocumentoId,
						NumeracionDocumento = adjuntarFormulario53Dto.Numeracion,
						Archivo = archivoByte
					});
				}
				else
				{
					formulario53.Archivo = archivoByte;

					_context.Attach<DocumentoTransaccion>(formulario53);
					_context.Entry<DocumentoTransaccion>(formulario53).State = EntityState.Modified;
					_context.Update(formulario53);
				}
					
			}
			else
			{
				await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
				{
					TransaccionId = adjuntarFormulario53Dto.Id,
					TipoDocumentoId = adjuntarFormulario53Dto.TipoDocumentoId,
					NumeracionDocumento = adjuntarFormulario53Dto.Numeracion,
					Archivo = archivoByte
				});
			}

			await _context.SaveChangesAsync();
		}

		public byte[] GenerateFormulario53(InputTransaccionReport53 InputTransaccionReport53)
		{
			var formulario53 = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(1, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(11));

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
							t.Span($"1.- LAS PROPIEDADES DETALLADAS A CONTINUACIÓN FUERON: Recibido(a) por el/la ${InputTransaccionReport53.RecibidoParam1}, \nEN LA FECHA CITADA");

							if (String.IsNullOrEmpty(InputTransaccionReport53.RecibidoParam2))
							{
								t.Span(".");
							}
							else
							{
								t.Span(" Y CUYA CONFORMIDAD CERTIFICÓ: ");
								t.Span(InputTransaccionReport53.RecibidoParam2).Bold();
							}

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
								d.ConstantColumn(3, Unit.Centimetre);
								d.ConstantColumn(2, Unit.Centimetre);
								d.ConstantColumn(10, Unit.Centimetre);
								d.RelativeColumn();
							});

							table.Header(header =>
							{
								header.Cell().Element(CellStyle).AlignCenter().Text("CANTIDAD");
								header.Cell().Element(CellStyle).AlignCenter().Text("UNIDAD");
								header.Cell().Element(CellStyle).AlignCenter().Text("DETALLE");
								header.Cell().Element(CellStyle).AlignLeft().Text("OBSERVACIONES");

							});

							foreach (var item in InputTransaccionReport53.Articulos)
							{
								table.Cell().Element(CellStyle).AlignCenter().Text(item.Cantidad.ToString());
								table.Cell().Element(CellStyle).AlignCenter().Text("UNA");
								table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(3).Text($"{item.SubTipo} {item.Marca} Cal. {item.Calibre}, No. | {item.Serie}");
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
									table.Cell().Column(2).Row((uint)(template_rows_start + item)).Element(CellStyle).Text("NOTA: ").AlignCenter().Bold();
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
											t.Span(InputTransaccionReport53.Comentario);
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
								t.Span(InputTransaccionReport53.EncargadoDepositos.NombreApellidoCompleto).Bold();
								t.Span($"\n{InputTransaccionReport53.EncargadoDepositos.Rango}"); // Rango encargado depositos
								t.Span("\nEnc. Rec. Y Ent. de Armas IGMBFA.");
								t.AlignCenter();
							});

							row.RelativeItem()
							.AlignRight()
							.Text(t =>
							{
								t.Span(InputTransaccionReport53.EncargadoArmas.NombreApellidoCompleto).Bold();
								t.Span($"\n{InputTransaccionReport53.EncargadoArmas.Rango}"); // Rango encargado armas
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
								.PaddingBottom(10, Unit.Point)
								.LineHorizontal(1, Unit.Point)
								.LineColor(Colors.Black);

								col.Item()
								.AlignCenter()
								.Text(t =>
								{
									t.Span($"{InputTransaccionReport53.Intendente.NombreApellidoCompleto}\n").Bold();
									t.Span($"{InputTransaccionReport53.Intendente.Rango}\n");
									t.Span("Int. Gral. Material Belico de las FFAA");
								});
							});

						});

					});

				});
			}).GeneratePdf();
			return formulario53;
		}


		public async Task<PagedData<TransaccionViewModel>> GetTransacciones(TransaccionPaginationFilter filters, int CodInstitucion, string[] roles)
		{
			string query = @$"
                            select 
                            T.Id,
                            (
                                case
                                    when T.TipoOrigen = 1 then T.Origen
                                    when T.TipoOrigen = 3 then iif(T.Origen = 'N/A', 'N/A', (select D.Nombre from Inv.Depositos as D where D.Id = cast(T.Origen as int))) 
                                    else 'NO DISPONIBLE'
                                end
                            ) as Origen,
                            (
                                case
                                    when T.TipoDestino = 1 then T.Destino
                                    when T.TipoDestino = 3 then iif(T.Destino = 'N/A', 'N/A', (select D.Nombre from Inv.Depositos as D where D.Id = cast(T.Destino as int))) 
                                    else 'NO DISPONIBLE'
                                end
                            ) as Destino,
                            T.FechaEfectividad as Fecha,
                            isnull(DT.NumeracionDocumento, 'NO DEFINIDO') as Formulario,
							cast(iif(DT.Archivo is null, 0, 1) as bit) as Tiene53,
							concat(iif(U.Institucion = 3, R.NombreArmada, R.Nombre), ', ', U.Nombre, ' ', U.Apellido) as Usuario,
							T.FechaCreacion
                            from Belico.Transacciones as T
                            left join Belico.DocumentosTransaccion as DT on DT.TransaccionId = T.Id and DT.TipoDocumentoId = {(int)TipoDocumentoEnum.FORMULARIO_53}
							left join accesos.Usuarios as U on U.Id = T.UsuarioId
							left join Personal.Rangos as R on R.Id = U.RangoId
                            where T.CodInstitucion = {CodInstitucion}
                            ";

			if (!String.IsNullOrEmpty(filters.Formulario53)) query += $" and DT.NumeracionDocumento like '%{filters.Formulario53}%' ";
			if (!String.IsNullOrEmpty(filters.Origen)) query += $" and T.Origen like '%{filters.Origen}%' ";
			if (!String.IsNullOrEmpty(filters.Destino)) query += $" and T.Destino like '%{filters.Destino}%' ";
			if (filters.Adjunto53.HasValue && filters.Adjunto53.Value) query += $" and cast(iif(DT.Id is null, 0, 1) as bit) = {Convert.ToInt32(filters.Adjunto53)} ";
			if (filters.FechaInicial.HasValue && filters.FechaFinal.HasValue) query += $" and T.FechaEfectividad between '{filters.FechaInicial.Value.ToString("yyyy-MM-dd")}' and '{filters.FechaFinal.Value.ToString("yyyy-MM-dd")}' ";

			query += $" order by T.FechaEfectividad desc OFFSET {(filters.Page - 1) * filters.Size} ROWS FETCH NEXT {filters.Size} ROWS ONLY";

			var result = await _context.SP_Obtener_Listado_Transacciones.FromSqlRaw(query).ToListAsync<TransaccionViewModel>();

			return new PagedData<TransaccionViewModel>
			{
				Page = filters.Page,
				Size = filters.Size,
				Rows = result,
				TotalCount = result.Count
			};
		}

		public async Task<object?> GetTransaccionDetails(int Id)
		{
			var result = await _context.Transacciones
				.Include(t => t.DetallesTransaccion)
				.ThenInclude(dt => dt.Articulo)
				.ThenInclude(a => a.Marca)
				.Include(t => t.DetallesTransaccion)
				.ThenInclude(dt => dt.Articulo)
				.ThenInclude(a => a.Modelo)
				.Include(t => t.DetallesTransaccion)
				.ThenInclude(dt => dt.Articulo)
				.ThenInclude(a => a.Calibre)
				.Include(t => t.DetallesTransaccion)
				.ThenInclude(dt => dt.Articulo)
				.ThenInclude(a => a.Tipo)
				.Include(t => t.DetallesTransaccion)
				.ThenInclude(dt => dt.Articulo)
				.ThenInclude(a => a.SubTipo)
				.Include(t => t.DocumentosTransaccion)
				.ThenInclude(dt => dt.TipoDocumento)
				.FirstOrDefaultAsync(t => t.Id == Id);

			if (result is null) return null;

			if (result.TipoOrigen == TipoOrigenDestinoEnum.DEPOSITO)
			{
				result.Origen = (await _context.Depositos.FirstOrDefaultAsync(d => d.Id == Convert.ToInt32(result.Origen)))?.Nombre ?? "N/A";
			}

			if (result.TipoDestino == TipoOrigenDestinoEnum.DEPOSITO)
			{
				result.Destino = (await _context.Depositos.FirstOrDefaultAsync(d => d.Id == Convert.ToInt32(result.Destino)))?.Nombre ?? "N/A";
			}

			return new
			{
				Transaccion = new
				{
					Origen = result.Origen,
					Destino = result.Destino,
					FechaEfectividad = result.FechaEfectividad
				},
				Articulos = result.DetallesTransaccion.Select(dt => new
				{
					Id = dt.ArticuloId,
					Serie = dt.Articulo?.Serie,
					Marca = dt.Articulo?.Marca?.Nombre,
					Modelo = dt.Articulo?.Modelo?.Nombre,
					SubTipo = dt.Articulo?.SubTipo?.Nombre,
					Calibre = dt.Articulo?.Calibre?.Nombre,
					Tipo = dt.Articulo?.Tipo?.Nombre,
					dt.Cantidad
				}),
				Documentos = result.DocumentosTransaccion.Select(dt => new
				{
					dt.Id,
					Numeracion = dt.NumeracionDocumento,
					TipoDocumentoId = dt.TipoDocumentoId,
					TipoDocumento = dt.TipoDocumento?.Nombre
				})
			};
		}

		public async Task<List<SerieTransaccionItem>> GetTransaccionesBySerie(string serie)
		{
			string query = $@"
							select 
							T.Origen,
							T.Destino,							
							T.FechaEfectividad,
							DT.NumeracionDocumento as Formulario
							from Belico.DetallesArticuloTransaccion as DAT
							left join Belico.DocumentosTransaccion as DT on DT.TransaccionId = DAT.TransaccionId
							left join Inv.Articulos as A on A.Id = DAT.ArticuloId
							left join Belico.Transacciones as T on T.Id = DAT.TransaccionId
							where A.Serie = '{serie}'
							order by T.FechaEfectividad asc";

			var transacciones = await _context.SP_Obtener_Transacciones_Serie.FromSqlRaw(query).ToListAsync(); 

			return transacciones;
		}

		public async Task<List<ArticuloTransaccionItem>> GetArticulosOrigenTransaccion(TipoTransaccionEnum tipoOrigen, string origen)
		{
			string query = $@"
                            select 
							A.Id,
							A.Serie,
							M.Nombre as Marca,
							MO.Nombre as Modelo,
							ST.Nombre as SubTipo,
							C.Nombre as Calibre,
							DAT.Cantidad,							
							T.FechaEfectividad,
							DT.NumeracionDocumento as Formulario
							from Belico.DetallesArticuloTransaccion as DAT
							left join Inv.Articulos as A on A.Id = DAT.ArticuloId
							left join Misc.Marcas as M on M.Id = A.MarcaId
							left join Misc.Modelos as MO on MO.Id = A.ModeloId
							left join Misc.SubTipos as ST on ST.Id = A.SubTipoId
							left join Misc.Calibres as C on C.Id = A.CalibreId
							left join Belico.DocumentosTransaccion as DT on DT.TransaccionId = DAT.TransaccionId
							left join Belico.Transacciones as T on T.Id = DT.TransaccionId 
							where T.Destino like  '{origen}' + '%'
							order by T.FechaEfectividad asc ";

			var articulos = await _context.SP_Obtener_Articulos_Origen_Transaccion.FromSqlRaw(query).ToListAsync(); // Use FromSqlRaw instead of ExecuteSqlRawAsync
			
			return articulos;
		}


		#region Cargar por excel

		internal sealed class ArticuloItemMetadata
		{
			public int ArticuloId { get; set; }
			public int TransaccionId { get; set; }
			public int Cantidad { get; set; }
		}

		public async Task UploadRelacionArticulos(IFormFile File, InputOrigenDestinoDto inputOrigenDestinoDto)
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

						using (var transaction = await _context.Database.BeginTransactionAsync())
						{
							try
							{
								for (int row = start_row; row <= row_dimmession; row++)
								{
									var origen = worksheet.Cells[row, 1].Value.ToString(); // Cedula o Deposito
									var destino = worksheet.Cells[row, 2].Value?.ToString(); // Cedula o Deposito
									var categoria = worksheet.Cells[row, 3].Value?.ToString() ?? CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.NO_DEFINIDA);
									var tipo = worksheet.Cells[row, 4].Value?.ToString() ?? "NO DEFINIDO";
									var subtipo = worksheet.Cells[row, 5].Value?.ToString() ?? "NO DEFINIDO";
									var marca = worksheet.Cells[row, 6].Value?.ToString() ?? "NO DEFINIDA";
									var modelo = worksheet.Cells[row, 7].Value?.ToString() ?? "NO DEFINIDO";
									var calibre = worksheet.Cells[row, 8].Value?.ToString() ?? "1"; // No Definido
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

								await transaction.CommitAsync();
							}
							catch (Exception)
							{
								await transaction.RollbackAsync();
								throw;
							}

						}
					}
				}
			}

		}
		private async Task<int> GetIdAsync<T>(string key, DbSet<T> dbSet) where T : NamedEntityMetadata
		{
			if (!datosArticulosDict.TryGetValue(key, out int id))
			{
				id = (await dbSet.FirstOrDefaultAsync(e => e.Nombre.Contains(key)))?.Id ?? 1;
				datosArticulosDict[key] = id;
			}
			return id;
		}
		private async Task<int> InsertArticulo(string categoria, string tipo, string subtipo, string marca, string modelo, string calibre, string serie)
		{		
			var newArticulo = new Articulo
			{
				CategoriaId = await GetIdAsync(categoria, _context.Categorias),
				TipoId = await GetIdAsync(tipo, _context.Tipos),
				SubTipoId = await GetIdAsync(subtipo, _context.SubTipos),
				MarcaId = await GetIdAsync(marca, _context.Marcas),
				ModeloId = await GetIdAsync(modelo, _context.Modelos),
				CalibreId = await GetIdAsync(calibre, _context.Calibres),
				Serie = serie,
				EsSeriado = String.IsNullOrEmpty(serie) ? false : true,
			};

			var articulo = await _context.Articulos.AddAsync(newArticulo);

			await _context.SaveChangesAsync();

			return articulo.Entity.Id;
		}

		private async Task<int> InsertTransaccion(TipoOrigenDestinoEnum tipoOrigen, string origen, TipoOrigenDestinoEnum tipoDestino, string destino, DateTime fechaEfectividad)
		{
			string origenResult = tipoOrigen switch
			{
				TipoOrigenDestinoEnum.MILITAR => origen,
				TipoOrigenDestinoEnum.CIVIL => origen,
				TipoOrigenDestinoEnum.DEPOSITO => (await _context.Depositos.FirstOrDefaultAsync(d => d.Nombre.Contains(origen)))?.Id.ToString() ?? "N/A",
				TipoOrigenDestinoEnum.FUNCION => "N/A",
				_ => throw new BaseException("Tipo de origen no soportado", HttpStatusCode.BadRequest)
			};

			string destinoResult = tipoDestino switch
			{
				TipoOrigenDestinoEnum.MILITAR => destino,
				TipoOrigenDestinoEnum.CIVIL => destino,
				TipoOrigenDestinoEnum.DEPOSITO => (await _context.Depositos.FirstOrDefaultAsync(d => d.Nombre.Contains(destino)))?.Id.ToString() ?? "N/A",
				TipoOrigenDestinoEnum.FUNCION => "N/A",
				_ => throw new BaseException("Tipo de destino no soportado", HttpStatusCode.BadRequest)
			};

			var transaccion = await _context.Transacciones.AddAsync(new Transaccion
			{
				TipoOrigen = tipoOrigen,
				Origen = origenResult,
				TipoDestino = tipoDestino,
				Destino = destinoResult,

				FechaEfectividad = DateOnly.FromDateTime(fechaEfectividad),

				EncargadoDeposito = "N/A",
				EncargadoGeneral = "N/A",
				Intendente = "N/A"

			});

			await _context.SaveChangesAsync();

			return transaccion.Entity.Id;
		}

		private async Task InsertDetalleTransaccion(List<ArticuloItemMetadata> data)
		{
			foreach (var item in data)
			{
				await _context.DetallesArticuloTransaccion.AddAsync(new DetalleArticuloTransaccion
				{
					ArticuloId = item.ArticuloId,
					TransaccionId = item.TransaccionId,
					Cantidad = item.Cantidad
				});

				await _context.SaveChangesAsync();
			}

		}

		private async Task InsertDocumentoTransaccion(string numero, int transaccionId)
		{
			await _context.DocumentosTransaccion.AddAsync(new DocumentoTransaccion
			{
				NumeracionDocumento = $"F-53-{numero}",
				TransaccionId = transaccionId,
				TipoDocumentoId = (int)TipoDocumentoEnum.FORMULARIO_53,
				Archivo = null
			});

			await _context.SaveChangesAsync();
		}

		#endregion


	}
}
