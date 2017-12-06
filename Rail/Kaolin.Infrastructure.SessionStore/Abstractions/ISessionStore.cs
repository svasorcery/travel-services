namespace Kaolin.Infrastructure.SessionStore
{
    public interface ISessionStore
    {
        string Id { get; }

        void Store<T>(string key, T value);

        T Retrieve<T>(string key);
    }
}
