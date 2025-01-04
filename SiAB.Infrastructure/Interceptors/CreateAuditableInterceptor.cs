using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Interceptors
{
	public sealed class CreateAuditableInterceptor : SaveChangesInterceptor
	{
		private int? CodUsuario;
		private int? CodInstitucion;
		public CreateAuditableInterceptor(int? codUsuario = 0, int? codInstitucion = 0)
		{
			CodUsuario = codUsuario;
			CodInstitucion = codInstitucion;
		}

		public void SetParameters(int codUsuario, int codInstitucion)
		{
			CodUsuario = codUsuario;
			CodInstitucion = codInstitucion;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			var entries = eventData.Context.ChangeTracker.Entries<IAuditableEntityMetadata>().Where(e => e.State == EntityState.Added && e.Entity.GetType() is IAuditableEntityMetadata);

			foreach (var entry in entries)
			{
				if (entry.Entity is IAuditableEntityMetadata auditable)
				{
					auditable.UsuarioId = CodUsuario ?? 0;
					auditable.CodInstitucion = (InstitucionEnum)CodInstitucion;
					auditable.FechaCreacion = DateTime.Now;
				}
			}

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
