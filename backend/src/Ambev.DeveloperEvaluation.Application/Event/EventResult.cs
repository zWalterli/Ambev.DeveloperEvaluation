namespace Ambev.DeveloperEvaluation.Application.Event;

public class EventResult
{
    public EventResult(bool sent)
    {
        Sent = sent;
    }

    public bool Sent { get; set; }
}