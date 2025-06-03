using Evently.Modules.Events.Application.Abstractions.Clock;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;
using FluentValidation;
using MediatR;

namespace Evently.Modules.Events.Application.Events;

public sealed record CreateEventCommand(
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand<Guid>;

public sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.StartsAtUtc).NotEmpty();
        RuleFor(c => c.EndsAtUtc)
            .Must((cmd, endsAtUtc) => endsAtUtc > cmd.StartsAtUtc)
            .When(c => c.EndsAtUtc.HasValue);
    }
}

internal sealed class CreateEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IEventRepository eventRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure<Guid>(EventErrors.StartDateInPast);
        }
        
        Result<Event> result = Event.Create(
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        eventRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
