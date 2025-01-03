using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Newtonsoft.Json;
using SiAB.Core.Models.RegistroDebitoCredito;
using SiAB.Core.Enums;

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

			builder.Entity<Usuario>(e => e.ToTable("Usuarios", "accesos"));
			builder.Entity<Role>(e => e.ToTable("Roles", "accesos"));

			#region Relations configuration
			
			builder.Entity<Articulo>()
				.HasOne(a => a.Modelo)
				.WithMany(m => m.Articulos)
				.HasForeignKey(a => a.ModeloId)
				.OnDelete(DeleteBehavior.NoAction);
			
			builder.Entity<SubTipo>()
				.HasOne(st => st.Tipo)
				.WithMany(t => t.SubTipos)
				.HasForeignKey(st => st.TipoId)
				.OnDelete(DeleteBehavior.NoAction);
			
			builder.Entity<Tipo>()
				.HasOne(t => t.Categoria)
				.WithMany(c => c.Tipos)
				.HasForeignKey(t => t.CategoriaId)
				.OnDelete(DeleteBehavior.NoAction);	
			
			builder.Entity<Alerta>()
				.HasOne(a => a.Serie)
				.WithMany(s => s.Alertas)
				.HasForeignKey(a => a.SerieId)
				.OnDelete(DeleteBehavior.NoAction);
			
			builder.Entity<Alerta>()
				.HasOne(a => a.Articulo)
				.WithMany(a => a.Alertas)
				.HasForeignKey(a => a.ArticuloId)
				.OnDelete(DeleteBehavior.NoAction);
			
			builder.Entity<Serie>()
				.HasOne(s => s.Articulo)
				.WithMany(a => a.Series)
				.HasForeignKey(s => s.ArticuloId)
				.OnDelete(DeleteBehavior.NoAction);
			
			builder.Entity<Serie>()
				.HasOne(s => s.Propiedad)
				.WithMany(p => p.Series)
				.HasForeignKey(s => s.PropiedadId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Modelo>()
				.HasOne(s => s.Marca)
				.WithMany(p => p.Modelos)
				.HasForeignKey(s => s.MarcaId)
				.OnDelete(DeleteBehavior.NoAction);

			#endregion

			ConfigureRegistroDebitoCreditoo(builder.Entity<RegistroDebitoCredito>());

			SeedUserRoleData(builder);
			
			SetGlobalQueryFilter(builder);
		}

		public void ConfigureRegistroDebitoCreditoo(EntityTypeBuilder<RegistroDebitoCredito> builder)
		{
			var options = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			builder.Property<IList<RegistroDebitoModel>?>(p => p.Articulos)
				.HasConversion(
					v => v == null ? null : JsonConvert.SerializeObject(v, options),
					v => v == null ? null : JsonConvert.DeserializeObject<IList<RegistroDebitoModel>>(v, options)
				);				
		}

		// Metodo para insertar datos de roles y usuarios
		private void SeedUserRoleData(ModelBuilder modelBuilder)
		{
			var passwordHasher = new PasswordHasher<Usuario>();
			
			modelBuilder.Entity<Role>().HasData(
				new Role { Id = (int)UsuarioRole.ADMIN, Name = RoleConstant.ADMIN, NormalizedName = RoleConstant.ADMIN },
				new Role { Id = (int)UsuarioRole.USER, Name = RoleConstant.USER, NormalizedName = RoleConstant.USER }
			);

			modelBuilder.Entity<Usuario>().HasData(
				new Usuario 
				{
				Id = 1,
				Cedula = "0000000000",
				Nombre = "Admin",
				Apellido = "Admin",
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Institucion = InstitucionEnum.MIDE,
					PasswordHash = passwordHasher.HashPassword(null, "admin01")
			});
			
			modelBuilder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int> { UserId = 1, RoleId = (int)UsuarioRole.ADMIN }
			);
		}

		// Configuramos un filtro global para las entidades, omitiendo registros borraddos (IsDeleted)
		private void SetGlobalQueryFilter(ModelBuilder modelBuilder)
		{
			// Obtenemos todas las clases que hereden de EntityMetadata (Entidades)
			var entities = modelBuilder.Model.GetEntityTypes()
				.Where(c => c.ClrType.IsAssignableTo(typeof(IAuditableEntityMetadata)));

			// omitir registros borrados al momento de hacer las consultas 
			Expression<Func<IAuditableEntityMetadata, bool>> filterExpression = e => !e.IsDeleted;

			foreach (var entity in entities)
			{
				var parameter = Expression.Parameter(entity.ClrType);
				var body = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.First(), parameter, filterExpression.Body);
				var lambda = Expression.Lambda(body, parameter);
				entity.SetQueryFilter(lambda);
			}
		}

		#region Entities

		#region Belico
		public DbSet<Alerta> Alertas { get; set; }
		public DbSet<Calibre> Calibres { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<RegistroDebitoCredito> RegistrosDebitoCredito { get; set; }
        public DbSet<Secuencia> Secuencias { get; set; }

        #endregion

        #region Miscelaneos
        public DbSet<Articulo> Articulos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Deposito> Depositos { get; set; }
		public DbSet<Marca> Marcas { get; set; }
		public DbSet<Modelo> Modelos { get; set; }
		public DbSet<Propiedad> Propiedades { get; set; }
		public DbSet<SubTipo> SubTipos { get; set; }
		public DbSet<Tipo> Tipos { get; set; }
        public DbSet<FotoModelo> FotoModelos { get; set; }
        #endregion

        #region Personal
        public DbSet<Rango> Rangos { get; set; }
		public DbSet<Dependencia> Dependencias { get; set; }

		#endregion

		#endregion
	}
}
