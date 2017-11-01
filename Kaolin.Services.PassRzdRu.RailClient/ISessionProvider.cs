using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    public interface ISessionProvider
    {
        ISessionStore Create();
        Task SaveAsync(ISessionStore session);
        Task<ISessionStore> LoadAsync(string id);
    }

    internal class InMemorySessionProvider : ISessionProvider
    {
        private readonly List<InMemorySessionStore> _sessions;

        public InMemorySessionProvider()
        {
            _sessions = new List<InMemorySessionStore>();
        }


        public ISessionStore Create()
        {
            var session = new InMemorySessionStore();
            _sessions.Add(session);
            return session;
        }

        public Task SaveAsync(ISessionStore session)
        {
            //var doc = _sessions.Find(s => s.Id == session.Id);
            //doc = (InMemorySessionStore)session;
            return Task.FromResult(0);
        }

        public Task<ISessionStore> LoadAsync(string id)
        {
            return Task.FromResult<ISessionStore>(_sessions.FirstOrDefault(x => x.Id == id));
        }
    }
}
