using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SiAB.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Interceptors
{
	public sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
	{
		private int? CodUsuario;
		public UpdateAuditableInterceptor(int? codUsuario = 0)
		{
			CodUsuario = codUsuario;
		}

		public void SetParameters(int codUsuario)
		{
			CodUsuario = codUsuario;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			var entries = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

			foreach (var entry in entries)
			{
				if (entry.Entity is IAuditableEntityMetadata auditable)
				{
					auditable.UsuarioIdModifico = CodUsuario ?? 0;
					auditable.FechaModificacion = DateTime.Now;
				}
			}

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
