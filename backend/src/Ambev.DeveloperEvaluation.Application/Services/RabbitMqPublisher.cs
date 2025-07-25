using Ambev.DeveloperEvaluation.Domain.Services;
using EasyNetQ;
using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class RabbitMqPublisher(IBus _bus) : IEventPublisherService
{
    public async Task SendAsync<T>(string queue, T message, CancellationToken cancellationToken) where T : class
    {
        var json = JsonConvert.SerializeObject(message);
        await _bus.SendReceive.SendAsync(queue, json, cancellationToken);
    }
}