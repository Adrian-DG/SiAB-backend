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
		private readonly int CodUsuario;
		private readonly int CodInstitucion;
		public CreateAuditableInterceptor(int codUsuario, int codInstitucion)
		{
			CodUsuario = codUsuario;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			var entries = eventData.Context.ChangeTracker.Entries<IAuditableEntityMetadata>().Where(e => e.State == EntityState.Added);

			foreach (var entry in entries)
			{
				if (entry.Entity is IAuditableEntityMetadata auditable)
				{
					auditable.UsuarioId = CodUsuario;
					auditable.CodInstitucion = (InstitucionEnum)CodInstitucion;
					auditable.FechaCreacion = DateTime.Now;
				}
			}

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
