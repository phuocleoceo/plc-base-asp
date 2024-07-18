using Quartz;

namespace PlcBase;

public class ExampleJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Truong Minh Phuoc !");
        return Task.CompletedTask;
    }
}
