using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Infrastructure.Repositories;
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
		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}
		
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
		
		public INamedRepository<T> NamedRepository<T>() where T : NamedMetadata
		{
			repositories ??= [];

			var type = typeof(T).Name;

			if (!repositories.ContainsKey(type))
			{
				var repositoryType = typeof(Repository<>);
				object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context)!;
				repositories.Add(type, repositoryInstance);
			}

			return (INamedRepository<T>)repositories[type];
		}
		
	}
}
