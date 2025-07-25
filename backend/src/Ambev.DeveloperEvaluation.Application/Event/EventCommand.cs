using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Event;

public class EventCommand : IRequest<EventResult>
{
    public EventCommand(Guid id, SaleEvent saleEvent)
    {
        Id = id;
        Event = saleEvent;

        EventQueueName = saleEvent switch
        {
            SaleEvent.SaleCreated => Domain.Utils.EventQueueName.SaleCreated,
            SaleEvent.SaleUpdated => Domain.Utils.EventQueueName.SaleUpdated,
            SaleEvent.SaleCancelled => Domain.Utils.EventQueueName.SaleCancelled,
            SaleEvent.SaleItemCancelled => Domain.Utils.EventQueueName.ItemCancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(saleEvent), saleEvent, null)
        };
    }

    public Guid Id { get; set; }
    public SaleEvent Event { get; set; }
    public string EventQueueName { get; set; }
}
