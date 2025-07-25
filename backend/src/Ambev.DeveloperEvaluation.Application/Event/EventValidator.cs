using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Event;

public class EventValidator : AbstractValidator<EventCommand>
{
    public EventValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.EventQueueName)
            .NotEmpty().WithMessage("Event queue name is required.");

        RuleFor(x => x.Event)
            .IsInEnum()
            .WithMessage("Invalid event type.");
    }
}