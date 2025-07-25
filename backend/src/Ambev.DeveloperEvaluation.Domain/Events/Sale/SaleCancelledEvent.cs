using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale;

public record SaleCancelledEvent(Guid SaleId) : INotification;