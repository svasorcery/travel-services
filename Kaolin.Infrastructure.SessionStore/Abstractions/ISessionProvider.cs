using System;
using System.Threading.Tasks;

namespace Kaolin.Infrastructure.SessionStore
{
    public interface ISessionProvider
    {
        ISessionStore Create(TimeSpan ttl);
        Task SaveAsync(ISessionStore session);
        Task<ISessionStore> LoadAsync(string id);
    }
}
