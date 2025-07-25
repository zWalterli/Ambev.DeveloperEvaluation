namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IEventPublisherService
{
    Task SendAsync<T>(string queue, T message, CancellationToken cancellationToken) where T : class;
}
