using System;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    public interface ISessionStore
    {
        string Id { get; }

        void Store<T>(string key, T value);

        T Retrieve<T>(string key);
    }

    internal class InMemorySessionStore : ISessionStore
    {
        private readonly string _id;
        private readonly IDictionary<string, object> _store;

        public InMemorySessionStore()
        {
            _id = Guid.NewGuid().ToString();
            _store = new Dictionary<string, object>();
        }

        public string Id => _id;

        public void Store<T>(string key, T value) => _store.Add(key, value);

        public T Retrieve<T>(string key) => _store.ContainsKey(key) ? (T)_store[key] : default(T);
    }
}
