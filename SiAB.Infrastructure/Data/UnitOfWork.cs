using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Infrastructure.Repositories;
using SiAB.Infrastructure.Repositories.Belico;
using SiAB.Infrastructure.Repositories.JCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
    {
		private Dictionary<string, object> repositories;

		private readonly AppDbContext _context;
		private DapperContext _dapper;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
			_dapper = new DapperContext();
		}

		public IJCERepository JCERepository => new JCERepository();

		public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);

		public IRoleRepository RoleRepository => new RoleRepository(_context);

		public IReportRepository ReportRepository => new ReportRepository(_context);

		public ISipffaaRepository SipffaaRepository => new SipffaaRepository(_dapper);

		public ITransaccionRepository TransaccionRepository => new TrasaccionRepository(_context);

		public ISecuenciaRepository SecuenciaRepository => new SecuenciaRepository(_context);

		public IRepository<T> Repository<T>() where T : EntityMetadata
		{
			repositories ??= [];

			var type = typeof(T).Name;

			if (!repositories.ContainsKey(type))
			{
				var repositoryType = typeof(Repository<>);
				object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context)!;
				repositories.Add(type, repositoryInstance);
			}

			return (IRepository<T>)repositories[type];
		}
		
		
	}
}
