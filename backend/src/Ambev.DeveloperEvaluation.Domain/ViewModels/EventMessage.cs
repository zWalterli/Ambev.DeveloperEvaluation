using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.ViewModels;

public class EventMessage
{
    public EventMessage(Guid id, SaleEvent message)
    {
        Id = id;
        Message = message;
    }

    public Guid Id { get; }
    public SaleEvent Message { get; }
}