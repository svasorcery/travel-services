using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kaolin.Infrastructure.SessionStore
{
    internal class InMemorySessionProvider : ISessionProvider
    {
        private readonly List<InMemorySessionStore> _sessions;

        public InMemorySessionProvider()
        {
            _sessions = new List<InMemorySessionStore>();
        }


        public ISessionStore Create()
        {
            return new InMemorySessionStore();
        }

        public Task SaveAsync(ISessionStore session)
        {
            _sessions.Add((InMemorySessionStore)session);

            return Task.FromResult(0);
        }

        public Task<ISessionStore> LoadAsync(string id)
        {
            return Task.FromResult<ISessionStore>(_sessions.FirstOrDefault(x => x.Id == id));
        }
    }
}
