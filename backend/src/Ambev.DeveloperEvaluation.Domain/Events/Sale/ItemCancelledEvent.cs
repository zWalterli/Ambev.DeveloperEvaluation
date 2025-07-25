using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

public record ItemCancelledEvent(Guid ItemId) : INotification;