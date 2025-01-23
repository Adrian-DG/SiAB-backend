using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Data;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/transacciones")]
	[ApiController]
	public class TransaccionesController : GenericController<Transaccion>
	{
		public TransaccionesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		internal sealed class ArticuloItemMetadata
		{
			public int ArticuloId { get; set; }
			public int TransaccionId { get; set; }
			public int Cantidad { get; set; }
		}

		[HttpPost("upload-relacion-articulos")]
		public async Task<IActionResult> UploadRelacionArticulos([FromForm] IFormFile file)
		{
			if (file is null) throw new BaseException("No se ha enviado un archivo", HttpStatusCode.BadRequest);

			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);

				using (ExcelPackage excel = new ExcelPackage(stream))
				{
					var workBook = excel.Workbook;

					foreach (var worksheet in workBook.Worksheets)
					{
						var articulosData = new List<ArticuloItemMetadata>();

						for (int row = 2; row < worksheet.Dimension.End.Row; row++)
						{
							var cedula = worksheet.Cells[row, 1].Value?.ToString();
							var categoria = worksheet.Cells[row, 2].Value?.ToString();
							var tipo = worksheet.Cells[row, 3].Value?.ToString();
							var marca = worksheet.Cells[row, 4].Value?.ToString();
							var modelo = worksheet.Cells[row, 5].Value?.ToString();
							var calibre = worksheet.Cells[row, 6].Value?.ToString();
							var serie = worksheet.Cells[row, 7].Value?.ToString();
							var propiedad = worksheet.Cells[row, 8].Value?.ToString();
							var formulario53 = worksheet.Cells[row, 9].Value?.ToString();
							var cantidad = worksheet.Cells[row, 10].Value?.ToString();
							var fechaEfectividad = worksheet.Cells[row, 11].Value?.ToString();

							using (var context = new AppDbContext())
							{

								var articuloId = await InsertArticulo(
									categoria: categoria, 
									tipo: tipo, 
									marca: marca, 
									modelo: modelo, 
									calibre: calibre, 
									serie: serie, 
									propiedad: propiedad);
								
								var transaccionId = await InsertTransaccion(
									tipoOrigen: TipoTransaccionEnum.DEPOSITO, 
									origen: "N/A", 
									tipoDestino: TipoTransaccionEnum.MIEMBRO, 
									destino: cedula, 
									intendente: "Intendente", 
									encargadoGeneral: "Encargado General", 
									encargadoDeposito: "Encargado Deposito", 
									entregadoA: "Entregado A", 
									recibidoPor: "Recibido Por", 
									firmadoPor: "Firmado Por", 
									fechaEfectividad: DateTime.Parse(fechaEfectividad));

								articulosData.Add(new ArticuloItemMetadata { ArticuloId = articuloId, TransaccionId = transaccionId, Cantidad = int.Parse(cantidad) });
							}
						}

						await InsertDetalleTransaccion(articulosData);
					}
				}
			}

			return Ok();
		}


		private async Task<int> InsertArticulo(string categoria, string tipo, string marca, string modelo, string calibre, string serie, string propiedad)
		{
			using (var context = new AppDbContext())
			{
				// Define output parameter
				var outputParamter = new SqlParameter("@ArticuloId", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

				// Prepare other parameters
				var parameters = new List<SqlParameter>
								{
									new SqlParameter("@Categoria", categoria),
									new SqlParameter("@Tipo", tipo),
									new SqlParameter("@Marca", marca),
									new SqlParameter("@Modelo", modelo),
									new SqlParameter("@Calibre", calibre),
									new SqlParameter("@Serie", serie),
									new SqlParameter("@Propiedad", propiedad),
									new SqlParameter("@CodUsuario", _codUsuario),
									new SqlParameter("@CodInstitucionUsuario", _codInstitucionUsuario),
									outputParamter
								};

				// Execute stored procedure
				await context.Database.ExecuteSqlRawAsync("EXEC [MISC].[Agregar_Articulo_Inventario] @Categoria, @Tipo, @Marca, @Modelo, @Calibre, @Serie, @Propiedad, @CodUsuario, @CodInstitucionUsuario, @ArticuloId OUTPUT", parameters.ToArray());

				return (int)outputParamter.Value;
			}

		}

		private async Task<int> InsertTransaccion(
			TipoTransaccionEnum tipoOrigen, string origen, TipoTransaccionEnum tipoDestino, string destino,
			string intendente, string encargadoGeneral, string encargadoDeposito, string entregadoA,
			string recibidoPor, string firmadoPor, DateTime fechaEfectividad)
		{
			using (var context = new AppDbContext())
			{
				// Prepare parameters
				var outputParameter = new SqlParameter("@TransaccionId", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };

				var parameters = new SqlParameter[] { 
					new SqlParameter("@TipoOrigen", tipoOrigen),
					new SqlParameter("@Origen", origen),
					new SqlParameter("@TipoDestino", tipoDestino),
					new SqlParameter("@Destino", destino),
					new SqlParameter("@Intendente", intendente),
					new SqlParameter("@EncargadoGeneral", encargadoGeneral),
					new SqlParameter("@EncargadoDeposito", encargadoDeposito),
					new SqlParameter("@EntregadoA", entregadoA),
					new SqlParameter("@RecibidoPor", recibidoPor),
					new SqlParameter("@FirmadoPor", firmadoPor),
					new SqlParameter("@FechaEfectividad", fechaEfectividad.ToString("yyyy-MM-dd")),
					new SqlParameter("@CodUsuario", _codUsuario),
					new SqlParameter("@CodInstitucionUsuario", _codInstitucionUsuario),
				};

				await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_transaccion] @TipoOrigen, @Origen, @TipoDestino, @Destino, @Intendente, @EncargadoGeneral, @EncargadoDeposito, @EntregadoA, @RecibidoPor, @FirmadoPor, @FechaEfectividad, @CodUsuario, @CodInstitucionUsuario, @TransaccionId OUTPUT", parameters);

				return (int)outputParameter.Value;

			}
		}

		private async Task InsertDetalleTransaccion(List<ArticuloItemMetadata> data)
		{
			var parameters = new SqlParameter[] { 
				new SqlParameter("@Articulos", data),
				new SqlParameter("@UsuarioId", _codUsuario),
				new SqlParameter("@CodInstitucion", _codInstitucionUsuario)
			};

			using (var context = new AppDbContext())
			{
				await context.Database.ExecuteSqlRawAsync("EXEC [Belico].[registrar_detalle_transaccion] @Articulos, @UsuarioId, @CodInstitucion", parameters);
			}
		}	


	}
}
