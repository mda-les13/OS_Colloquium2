public class MySingleton
{
    private MySingleton() { }

    public static MySingleton Instance => SingletonHolder<
        MySingleton,
        CreateUsingPrivateConstructor<MySingleton>,
        DefaultLifetime<MySingleton>,
        SingleThreaded<MySingleton>
    >.Instance;

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void Dispose()
    {
        Console.WriteLine("MySingleton instance disposed.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var singleton1 = MySingleton.Instance;
        var singleton2 = MySingleton.Instance;

        Console.WriteLine("singleton1 and singleton2 are the same instance: " + ReferenceEquals(singleton1, singleton2));

        singleton1.PrintMessage("Hello, Singleton!");

        Task task1 = Task.Run(() =>
        {
            var threadSingleton = MySingleton.Instance;
            threadSingleton.PrintMessage("Task 1 accessing Singleton");
        });

        Task task2 = Task.Run(() =>
        {
            var threadSingleton = MySingleton.Instance;
            threadSingleton.PrintMessage("Task 2 accessing Singleton");
        });

        Task.WaitAll(task1, task2);

        Console.WriteLine("Press any key to exit and trigger destruction...");
        Console.ReadKey();
    }
}
