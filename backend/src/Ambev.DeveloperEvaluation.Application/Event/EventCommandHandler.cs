using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Event;

public class EventCommandHandler(
    ILogger<EventCommandHandler> _logger,
    IEventPublisherService _eventPublisherService) : IRequestHandler<EventCommand, EventResult>
{
    public async Task<EventResult> Handle(EventCommand request, CancellationToken cancellationToken)
    {
        var validator = new EventValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Processing event with ID: {Id} and Type: {EventType}", request.Id, request.Event);
        var eventMessage = new EventMessage(request.Id, request.Event);

        await _eventPublisherService.SendAsync(request.EventQueueName, eventMessage, cancellationToken);
        _logger.LogInformation("Event message created: {EventMessage}", eventMessage);

        return new EventResult(true);
    }
}
