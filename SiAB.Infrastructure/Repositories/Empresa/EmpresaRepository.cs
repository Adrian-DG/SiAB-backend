using SiAB.Application.Contracts;
using SiAB.Core.DTO.Empresa;
using SiAB.Core.Entities.Empresa;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
						FechaEfectividad = createOrdenEmpresaDto.FechaEfectividad,
						Comentario = createOrdenEmpresaDto.Comentario,
						EmpresaId = Id
					});

					await _context.SaveChangesAsync();

					// Create OrdenEmpresaArticulo & OrdenEmpresaDetalle

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
							Cantidad = item.Cantidad,
							EmpresaId = Id
						});

						await _context.SaveChangesAsync();

						var ordenDetalle = await _context.OrdenesEmpresaDetalles.AddAsync(new OrdenEmpresaDetalle
						{
							OrdenEmpresaArticulo = articulo.Entity.Id,
							OrdenEmpresaId = orden.Entity.Id
						});

						await _context.SaveChangesAsync();
					}

					if (createOrdenEmpresaDto.Documentos == null) throw new Exception("No se han especificado documentos para la orden");

					foreach (CreateOrdenEmpresaDocumentoDto item in createOrdenEmpresaDto.Documentos)
					{
						if (item.Archivo == null) throw new Exception("No se ha especificado un archivo para el documento");

						string archivoBase64 = DateUrlToBase64Converter.ConvertDataUrlToBase64String(item.Archivo);
						byte[] archivoBytes = Convert.FromBase64String(archivoBase64);

						await _context.OrdenesEmpresDocumentos.AddAsync(new OrdenEmpresaDocumento
						{
							TipoDocumentoId = item.TipoDocumentoId,
							Archivo = archivoBytes,
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



	}
}
