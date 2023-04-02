using Savorboard.CAP.InMemoryMessageQueue;
using PlcBase.Shared.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class MessageQueueExtension
{
    public static void ConfigureCapQueue(this IServiceCollection services, IConfiguration configuration)
    {
        CapSettings capSettings = configuration.GetSection("CapSettings").Get<CapSettings>();

        if (capSettings.Enable)
        {
            services.AddCap(capOptions =>
            {
                capOptions.UseInMemoryStorage();
                capOptions.UseInMemoryMessageQueue();
            });
        }
    }
}