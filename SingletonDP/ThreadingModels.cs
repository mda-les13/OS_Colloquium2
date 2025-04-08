public interface IThreadingModel<T>
{
    void Lock();
    void Unlock();
}

public class SingleThreaded<T> : IThreadingModel<T>
{
    public void Lock() { }
    public void Unlock() { }
}
