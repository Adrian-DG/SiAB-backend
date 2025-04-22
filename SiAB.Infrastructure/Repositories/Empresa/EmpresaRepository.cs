using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Empresa;
using SiAB.Core.Entities.Empresa;
using SiAB.Core.Exceptions;
using SiAB.Core.Models.Empresa;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Repositories.Empresa
{
	public class EmpresaRepository : IEmpresaRepository
	{
		private readonly AppDbContext _context;
		public EmpresaRepository(AppDbContext dbContext)
		{
			_context = dbContext;
		}

		public async Task CreateEmpresa(CreateEmpresaDto createEmpresaDto)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var empresa = await _context.Empresas.AddAsync(new Core.Entities.Empresa.Empresa
					{
						Nombre = createEmpresaDto.Nombre,
						RNC = createEmpresaDto.RNC
					});

					await _context.SaveChangesAsync();

					if (createEmpresaDto.Titulares is not null)
					{
						foreach (CreateTitularDto item in createEmpresaDto.Titulares)
						{
							await _context.Titulares.AddAsync(new Core.Entities.Empresa.Titular
							{
								Identificacion = item.Identificacion,
								Nombre = item.Nombre,
								Apellido = item.Apellido,
								EmpresaId = empresa.Entity.Id
							});

							await _context.SaveChangesAsync();
						}						
					}

					if (createEmpresaDto.Telefonos is not null)
					{
						foreach (CreateContactoDto item in createEmpresaDto.Telefonos)
						{
							await _context.Contactos.AddAsync(new Core.Entities.Empresa.Contacto
							{
								Telefono = item.Telefono ?? "",
								EmpresaId = empresa.Entity.Id
							});

							await _context.SaveChangesAsync();
						}						
					}

					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public async Task CreateOrdenEmpresa(int Id, CreateOrdenEmpresaDto createOrdenEmpresaDto)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					// Create OrdenEmpresa 

					var orden = await _context.OrdenesEmpresa.AddAsync(new OrdenEmpresa
					{
						FechaEfectividad = DateOnly.Parse(createOrdenEmpresaDto.FechaEfectividad),
						Comentario = createOrdenEmpresaDto.Comentario,
						EmpresaId = Id
					});

					await _context.SaveChangesAsync();

					// Create OrdenEmpresaArticulo

					if (createOrdenEmpresaDto.Articulos == null) throw new Exception("No se han especificado articulos para la orden");
					
					foreach (CreateOrdenEmpresaArticuloDto item in createOrdenEmpresaDto.Articulos)
					{
						var articulo = await _context.OrdenesEmpresaArticulos.AddAsync(new OrdenEmpresaArticulo
						{
							CategoriaId = item.CategoriaId,
							TipoId = item.TipoId,
							SubTipoId = item.SubTipoId,
							MarcaId = item.MarcaId,
							ModeloId = item.ModeloId,
							CalibreId = item.CalibreId,
							Serie = item.Serie,
							CantidadRecibida = item.Cantidad,
							OrdenEmpresaId = orden.Entity.Id,
						});

						await _context.SaveChangesAsync();
					}

					if (createOrdenEmpresaDto.Documentos == null) throw new Exception("No se han especificado documentos para la orden");

					foreach (CreateOrdenEmpresaDocumentoDto item in createOrdenEmpresaDto.Documentos)
					{
						if (item.Archivo == null) throw new Exception("No se ha especificado un archivo para el documento");

						string archivoBase64 = DataUrlToBase64Converter.ConvertDataUrlToBase64String(item.Archivo);
						byte[] archivoBytes = Convert.FromBase64String(archivoBase64);

						await _context.OrdenesEmpresDocumentos.AddAsync(new OrdenEmpresaDocumento
						{
							TipoDocumentoId = item.TipoDocumentoId,
							NombreArchivo = item.Nombre,
							Archivo = archivoBytes,
							FechaEmision = DateOnly.Parse(item.FechaEmision),
							FechaRecepcion = DateOnly.Parse(item.FechaRecepcion),
							FechaExpiracion = DateOnly.Parse(item.FechaExpiracion),
							OrdenEmpresaId = orden.Entity.Id
						});

						await _context.SaveChangesAsync();
					}

					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}
		

		public async Task<object> GetDetalleOrdenEmpresa(int OrdenId)
		{
			var orden = await _context.OrdenesEmpresa
				.Include(o => o.Articulos)
				.ThenInclude(a => a.Categoria)
				.Include(o => o.Articulos)
				.ThenInclude(a => a.Tipo)
				.Include(o => o.Articulos)
				.ThenInclude(a => a.SubTipo)
				.Include(o => o.Articulos)
				.ThenInclude(a => a.Marca)
				.Include(o => o.Articulos)
				.ThenInclude(a => a.Calibre)
				.Include(o => o.Documentos)
				.ThenInclude(d => d.TipoDocumento)
				.FirstOrDefaultAsync(o => o.Id == OrdenId);

			if (orden is null) throw new Exception("No se ha encontrado la orden de empresa");

			return new OrdenEmpresaDetailModel
			{
				Id = orden.Id,
				Comentario = orden.Comentario,
				FechaEfectividad = orden.FechaEfectividad,
				Articulos = orden.Articulos.Select(a => new OrdenEmpresaArticuloModel
				{
					Id = a.Id,
					Categoria = a.Categoria.Nombre,
					Tipo = a.Tipo.Nombre,
					SubTipo = a.SubTipo.Nombre,
					Marca = a.Marca.Nombre,
					Calibre = a.Calibre.Nombre,
					Serie = a.Serie,
					CantidadRecibida = a.CantidadRecibida,
					CantidadEntregada = a.CantidadEntregada
				}).ToList(),
				Documentos = orden.Documentos.Select(d => new OrdenEmpresaDocumentoDetailModel
				{
					Id = d.Id,
					NombreArchivo = d.NombreArchivo,
					DataUrl = d.DocumentDataUrl,
					TipoDocumento = d.TipoDocumento.Nombre,
					FechaEmision = d.FechaEmision,
					FechaRecepcion = d.FechaRecepcion,
					FechaExpiracion = d.FechaExpiracion
				}).ToList()
			};

		}

		public async Task UpdateOrdenAdjuntarDocumento(int ordenId, CreateOrdenEmpresaDocumentoDto ordenEmpresaDocumentoDto)
		{
			if (string.IsNullOrEmpty(ordenEmpresaDocumentoDto.Archivo) || ordenEmpresaDocumentoDto.Archivo is null) throw new ArgumentNullException("El archivo es obligatorio");

			if (!DateOnly.TryParse(ordenEmpresaDocumentoDto.FechaEmision, out DateOnly FechaEmision) ||
				!DateOnly.TryParse(ordenEmpresaDocumentoDto.FechaRecepcion, out DateOnly FechaRecepcion) ||
				!DateOnly.TryParse(ordenEmpresaDocumentoDto.FechaExpiracion, out DateOnly FechaExpiracion))
			{
				throw new BaseException("Error al parsear campos requeridos", System.Net.HttpStatusCode.BadRequest);
			}

			string archivoBase64 = DataUrlToBase64Converter.ConvertDataUrlToBase64String(ordenEmpresaDocumentoDto.Archivo);
			byte[] archivoByteArray = Convert.FromBase64String(archivoBase64);

			var newOrdenEmpresaDoc = new OrdenEmpresaDocumento
			{
				TipoDocumentoId = ordenEmpresaDocumentoDto.TipoDocumentoId,
				NombreArchivo = ordenEmpresaDocumentoDto.Nombre,
				Archivo = archivoByteArray,
				OrdenEmpresaId = ordenId,
				FechaEmision = FechaEmision,
				FechaRecepcion = FechaRecepcion,
				FechaExpiracion = FechaExpiracion
			};

			await _context.OrdenesEmpresDocumentos.AddAsync(newOrdenEmpresaDoc);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateOrdenArticulosEntregados(int ordenId, List<OrdenEmpresaArticuloModel> ordenEmpresaArticulos)
		{
			var existingArticulos = await _context.OrdenesEmpresaArticulos
				.Where(a => a.OrdenEmpresaId == ordenId)
				.ToListAsync();

			var articuloDictionary = ordenEmpresaArticulos.ToDictionary(a => a.Id, a => a.CantidadEntregada);

			foreach (var articulo in existingArticulos)
			{
				if (articuloDictionary.TryGetValue(articulo.Id, out var cantidadEntregada))
				{
					articulo.CantidadEntregada = cantidadEntregada;
				}
			}

			await _context.SaveChangesAsync();
		}
	}
}
