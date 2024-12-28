using SiAB.API.Extensions;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Exceptions;
using System.Net;

namespace SiAB.API.Helpers
{
	public class NamedService : INamedService
	{
		private readonly IUnitOfWork _uow;
		public NamedService(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}
		public string Nombre { get; set; }

		public async Task<bool> ExistByNameAsync<T>(string name) where T : NamedEntityMetadata
		{
			var comparer = StringExtensions.CompareStringsExpression();
			
			var entities = await _uow.Repository<T>().GetListAsync(
					predicate: e => e.Nombre.Contains(name),
					selector: e => new { Nombre = e.Nombre },
					orderBy: e => e.OrderBy(p => p.Nombre),
					ignoreFilter: true
				);
			
			return entities.Any(p => comparer.Compile()(p.Nombre, name));
		}
	}
}
