using System;
using System.Threading.Tasks;

namespace Kaolin.Infrastructure.SessionStore
{
    public interface ISessionProvider
    {
        ISessionStore Create();
        Task SaveAsync(ISessionStore session);
        Task<ISessionStore> LoadAsync(string id);
    }
}
