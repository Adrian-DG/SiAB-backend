using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SiAB.API.Attributes;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			_connectionService = databaseConnectionService;
			optionsBuilder.UseSqlServer(_connectionService.GetConnectionString());
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

				return Ok(result);
			}
		}

		internal sealed class ArticuloItemMetadata
		{
			public int ArticuloId { get; set; }
			public int TransaccionId { get; set; }
			public int Cantidad { get; set; }
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
							var propiedad = worksheet.Cells[row, 1].Value.ToString(); // Institucion
							var origen = worksheet.Cells[row, 2].Value?.ToString(); // Deposito 
							var destino = worksheet.Cells[row, 3].Value?.ToString(); // Cedula/RNC

							var categoria = worksheet.Cells[row, 4].Value?.ToString();
							var tipo = worksheet.Cells[row, 5].Value?.ToString();
							var subtipo = worksheet.Cells[row, 6].Value?.ToString();
							var marca = worksheet.Cells[row, 7].Value?.ToString();
							var modelo = worksheet.Cells[row, 8].Value?.ToString();
							var calibre = worksheet.Cells[row, 9].Value?.ToString();
							var serie = worksheet.Cells[row, 10].Value?.ToString();
							var formulario53 = worksheet.Cells[row, 11].Value?.ToString();
							var cantidad = worksheet.Cells[row, 12].Value?.ToString();
							var fechaValue = worksheet.Cells[row, 13].Value?.ToString();

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
									serie: serie,
									propiedad: propiedad);

							var transaccionId = await InsertTransaccion(
								tipoOrigen: inputOrigenDestinoDto.Origen,
								origen: origen,
								tipoDestino: inputOrigenDestinoDto.Destino,
								destino: destino.ToString().Replace("-", ""),
								intendente: "00000000000",
								encargadoGeneral: "00000000000",
								encargadoDeposito: "00000000000",
								entregadoA: "00000000000",
								recibidoPor: "00000000000",
								firmadoPor: "00000000000",
								fechaEfectividad: fechaEfectividad);

							await InsertDocumentoTransaccion(numero: formulario53, transaccionId: transaccionId);

							articulosData.Add(new ArticuloItemMetadata { ArticuloId = articuloId, TransaccionId = transaccionId, Cantidad = int.Parse(cantidad) });
						
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


		private async Task<int> InsertArticulo(string categoria, string tipo, string subtipo, string marca, string modelo, string calibre, string serie, string propiedad)
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
					new SqlParameter("@Propiedad", propiedad),
					new SqlParameter("@CodUsuario", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario),
					outputParamter
				};

				// Execute stored procedure
				await context.Database.ExecuteSqlRawAsync("EXEC [MISC].[Agregar_Articulo_Inventario] @Categoria, @Tipo, @SubTipo, @Marca, @Modelo, @Calibre, @Serie, @Propiedad, @CodUsuario, @CodInstitucion, @ArticuloId OUTPUT", parameters.ToArray());

				return (int)outputParamter.Value;
			}
		}

		private async Task<int> InsertTransaccion(
			TipoTransaccionEnum tipoOrigen, string origen, TipoTransaccionEnum tipoDestino, string destino,
			string intendente, string encargadoGeneral, string encargadoDeposito, string entregadoA,
			string recibidoPor, string firmadoPor, DateTime fechaEfectividad)
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
					new SqlParameter("@Intendente", intendente),
					new SqlParameter("@EncargadoGeneral", encargadoGeneral),
					new SqlParameter("@EncargadoDeposito", encargadoDeposito),
					new SqlParameter("@EntregadoA", entregadoA),
					new SqlParameter("@RecibidoPor", recibidoPor),
					new SqlParameter("@FirmadoPor", firmadoPor),
					new SqlParameter("@FechaEfectividad", fechaEfectividad.ToString("yyyy-MM-dd")),
					new SqlParameter("@CodUsuario", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario),
					outputParameter
				};

				await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_transaccion] @TipoOrigen, @Origen, @TipoDestino, @Destino, @Intendente, @EncargadoGeneral, @EncargadoDeposito, @EntregadoA, @RecibidoPor, @FirmadoPor, @FechaEfectividad, @CodUsuario, @CodInstitucion, @TransaccionId OUTPUT", parameters);

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
					new SqlParameter("@Cantidad", item.Cantidad),
					new SqlParameter("@UsuarioId", _codUsuario),
					new SqlParameter("@CodInstitucion", _codInstitucionUsuario)
				};

				using (var context = new AppDbContext(optionsBuilder.Options))
				{
					await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_detalle_transaccion] @ArticuloId, @TransaccionId, @Cantidad, @UsuarioId, @CodInstitucion", parameters);
				}
			}
			
		}


	}
}
