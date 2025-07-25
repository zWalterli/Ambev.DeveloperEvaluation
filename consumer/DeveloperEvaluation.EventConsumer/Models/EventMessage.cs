using DeveloperEvaluation.EventConsumer.Utils;

namespace DeveloperEvaluation.EventConsumer.Models;

public class EventMessage
{
    public Guid Id { get; set; }
    public SaleEvent Message { get; set; }
}