public interface ICreationPolicy<T>
{
    T Create();
    void Destroy(T instance);
}

public class CreateUsingNew<T> : ICreationPolicy<T> where T : new()
{
    public T Create() => new T();
    public void Destroy(T instance) => (instance as IDisposable)?.Dispose();
}

public class CreateUsingPrivateConstructor<T> : ICreationPolicy<T>
{
    public T Create() => (T)Activator.CreateInstance(typeof(T), true);
    public void Destroy(T instance) => (instance as IDisposable)?.Dispose();
}