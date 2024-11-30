using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiAB.Core.Abstraction;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<Usuario>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			SetGlobalQueryFilter(builder);
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

        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Calibre> Calibres { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Rango> Rangos { get; set; }
        public DbSet<SubTipo> SubTipos { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        #endregion
    }
}
