using Savorboard.CAP.InMemoryMessageQueue;

using PlcBase.Shared.Helpers;

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
