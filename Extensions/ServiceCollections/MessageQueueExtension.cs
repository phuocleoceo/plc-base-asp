using Savorboard.CAP.InMemoryMessageQueue;

namespace PlcBase.Extensions.ServiceCollections;

public static class MessageQueueExtension
{
    public static void ConfigureCapQueue(this IServiceCollection services)
    {
        services.AddCap(capOptions =>
        {
            capOptions.UseInMemoryStorage();
            capOptions.UseInMemoryMessageQueue();
        });
    }
}