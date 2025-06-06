﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using System.Linq.Expressions;
using SiAB.Infrastructure.Data.Seed;
using SiAB.Core.ProcedureResults;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Models.Transacciones;
using SiAB.Core.Entities.Empresa;

namespace SiAB.Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<Usuario, Role, int>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			#region Usuario/Role entity configuration

			builder.Entity<Usuario>(e => e.ToTable("Usuarios", "accesos"));

			builder.Entity<Usuario>().HasIndex(u => u.Cedula).IsUnique();

			builder.Entity<Role>(e => e.ToTable("Roles", "accesos"));

			builder.Entity<ArticuloTransaccionItem>().HasNoKey();

			builder.Entity<SerieTransaccionItem>().HasNoKey();

			builder.Entity<TransaccionViewModel>().HasNoKey();

			#endregion

			// Fix for CS8602: Ensure null checks before accessing Articulo and Transaccion
			builder.Entity<HistorialUbicacion>().HasQueryFilter(h => h.Articulo != null && !h.Articulo.IsDeleted);
			builder.Entity<HistorialUbicacion>().HasQueryFilter(h => h.Transaccion != null && !h.Transaccion.IsDeleted);
			builder.Entity<StockArticulo>().HasQueryFilter(s => s.Articulo != null && !s.Articulo.IsDeleted);

			#region Relations configuration

			builder.Entity<Articulo>()
				.HasOne(a => a.Categoria)
				.WithMany(c => c.Articulos)
				.HasForeignKey(a => a.CategoriaId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Articulo>()
				.HasOne(a => a.Tipo)
				.WithMany(t => t.Articulos)
				.HasForeignKey(a => a.TipoId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Articulo>()
				.HasOne(a => a.SubTipo)
				.WithMany(t => t.Articulos)
				.HasForeignKey(a => a.SubTipoId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Articulo>()
				.HasOne(a => a.Marca)
				.WithMany(t => t.Articulos)
				.HasForeignKey(a => a.MarcaId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Articulo>()
				.HasOne(a => a.Modelo)
				.WithMany(m => m.Articulos)
				.HasForeignKey(a => a.ModeloId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Articulo>()
				.HasOne(a => a.Calibre)
				.WithMany(m => m.Articulos)
				.HasForeignKey(a => a.CalibreId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<DetalleArticuloTransaccion>().HasOne(d => d.Articulo);

			builder.Entity<DetalleArticuloTransaccion>()
				.HasOne(d => d.Transaccion)
				.WithMany(d => d.DetallesTransaccion)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<DocumentoTransaccion>()
				.HasOne(d => d.Transaccion)
				.WithMany(d => d.DocumentosTransaccion)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Tipo>()
				.HasOne(t => t.Categoria)
				.WithMany(c => c.Tipos)
				.HasForeignKey(t => t.CategoriaId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<SubTipo>()
				.HasOne(st => st.Tipo)
				.WithMany(t => t.SubTipos)
				.HasForeignKey(st => st.TipoId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Modelo>()
				.HasOne(s => s.Marca)
				.WithMany(p => p.Modelos)
				.HasForeignKey(s => s.MarcaId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<HistorialUbicacion>()
				.HasKey(c => c.Serie);

			builder.Entity<StockArticulo>()
				.HasKey(c => new { c.ArticuloId, c.TipoEntidad, c.Entidad });

			#endregion

			#region Data seeding

			RangoSeeder.Seed(builder);

			MiscelaneosSeeder.Seed(builder);

			RoleSeeder.Seed(builder);

			UserSeeder.Seed(builder);

			SetGlobalQueryFilter(builder);

			#endregion
		}

		// Configuramos un filtro global para las entidades, omitiendo registros borraddos (IsDeleted)
		private void SetGlobalQueryFilter(ModelBuilder modelBuilder)
		{
			// Obtenemos todas las clases que hereden de EntityMetadata (Entidades)
			var entities = modelBuilder.Model.GetEntityTypes()
				.Where(c => c.ClrType.IsAssignableTo(typeof(EntityMetadata)));

			// omitir registros borrados al momento de hacer las consultas 
			Expression<Func<EntityMetadata, bool>> filterExpression = e => !e.IsDeleted;

			foreach (var entity in entities)
			{
				var parameter = Expression.Parameter(entity.ClrType);
				var body = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.First(), parameter, filterExpression.Body);
				var lambda = Expression.Lambda(body, parameter);
				entity.SetQueryFilter(lambda);
			}
		}

		#region Entities

		#region Procedures

		public DbSet<ArticuloTransaccionItem> SP_Obtener_Articulos_Origen_Transaccion { get; set; }
		public DbSet<SerieTransaccionItem> SP_Obtener_Transacciones_Serie { get; set; }

		public DbSet<TransaccionViewModel> SP_Obtener_Listado_Transacciones { get; set; }

		#endregion

		#region Belico
		public DbSet<Alerta> Alertas { get; set; }
		public DbSet<Calibre> Calibres { get; set; }
        public DbSet<Secuencia> Secuencias { get; set; }
		public DbSet<Transaccion> Transacciones { get; set; }
		public DbSet<DetalleArticuloTransaccion> DetallesArticuloTransaccion { get; set; }
		public DbSet<DocumentoTransaccion> DocumentosTransaccion { get; set; }

		#endregion

		#region Inventario
		public DbSet<Articulo> Articulos { get; set; }
		public DbSet<Deposito> Depositos { get; set; }
		public DbSet<HistorialUbicacion> HistoricoUbicacion { get; set; }
		public DbSet<StockArticulo> StockArticulos { get; set; }

		#endregion

		#region Miscelaneos		
		public DbSet<Categoria> Categorias { get; set; }		
		public DbSet<Marca> Marcas { get; set; }
		public DbSet<Modelo> Modelos { get; set; }
		public DbSet<SubTipo> SubTipos { get; set; }
		public DbSet<Tipo> Tipos { get; set; }
		public DbSet<TipoDocumento> TipoDocumentos { get; set; }

		#endregion

		#region Personal
		public DbSet<Rango> Rangos { get; set; }
		public DbSet<Dependencia> Dependencias { get; set; }

		#endregion

		#region Empresa

		public DbSet<Empresa> Empresas { get; set; }
		public DbSet<Titular> Titulares { get; set; }
		public DbSet<OrdenEmpresa> OrdenesEmpresa { get; set; }
		public DbSet<OrdenEmpresaArticulo> OrdenesEmpresaArticulos { get; set; }
		public DbSet<OrdenEmpresaDocumento> OrdenesEmpresDocumentos { get; set; }
		public DbSet<Contacto> Contactos { get; set; }
		#endregion

		#endregion
	}
}
