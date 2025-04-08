using System;

public class SingletonHolder<T, TCreationPolicy, TLifetimePolicy, TThreadingModel>
    where TCreationPolicy : ICreationPolicy<T>, new()
    where TLifetimePolicy : ILifetimePolicy<T>, new()
    where TThreadingModel : IThreadingModel<T>, new()
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _destroyed;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        if (_destroyed)
                        {
                            new TLifetimePolicy().OnDeadReference();
                            _destroyed = false;
                        }

                        var creationPolicy = new TCreationPolicy();
                        _instance = creationPolicy.Create();

                        var lifetimePolicy = new TLifetimePolicy();
                        lifetimePolicy.ScheduleDestruction(DestroySingleton);
                    }
                }
            }
            return _instance;
        }
    }

    private static void DestroySingleton()
    {
        if (!_destroyed)
        {
            new TCreationPolicy().Destroy(_instance);
            _instance = default!;
            _destroyed = true;
        }
    }
}
