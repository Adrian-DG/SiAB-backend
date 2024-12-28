using SiAB.Core.Abstraction;
using SiAB.Core.DTO.Misc;

namespace SiAB.API.Helpers
{
	public interface INamedService
	{
        string Nombre { get; set; }
		Task<bool> ExistByNameAsync<T>(string name) where T : NamedEntityMetadata;
	}
}
