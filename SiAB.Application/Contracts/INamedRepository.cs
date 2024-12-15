using SiAB.Core.Abstraction;

namespace SiAB.Application.Contracts;

public interface INamedRepository<T> : IRepository<T> where T : NamedMetadata
{
    
}