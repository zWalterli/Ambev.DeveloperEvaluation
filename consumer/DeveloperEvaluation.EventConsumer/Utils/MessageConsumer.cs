using EasyNetQ;
using Newtonsoft.Json;

namespace DeveloperEvaluation.EventConsumer.Utils;

public static class MessageConsumer
{
    public static async Task RegisterJsonConsumer<T>(
        IBus bus,
        string queueName,
        Func<T, Task> handler
    ) where T : class
    {
        await bus.SendReceive.ReceiveAsync<string>(queueName, async json =>
        {
            try
            {
                var message = JsonConvert.DeserializeObject<T>(json);
                if (message is not null)
                    await handler(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message from {queueName}: {ex.Message}");
            }
        });
    }
}