public interface ILifetimePolicy<T>
{
    void ScheduleDestruction(Action destroyAction);
    void OnDeadReference();
}

public class DefaultLifetime<T> : ILifetimePolicy<T>
{
    public void ScheduleDestruction(Action destroyAction)
        => AppDomain.CurrentDomain.ProcessExit += (sender, args) => destroyAction();

    public void OnDeadReference()
        => throw new InvalidOperationException("Object has been destroyed");
}
