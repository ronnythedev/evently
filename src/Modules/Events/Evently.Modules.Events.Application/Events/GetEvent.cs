using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
namespace Evently.Modules.Events.Application.Events;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                id AS {nameof(EventResponse.Id)},
                title AS {nameof(EventResponse.Title)},
                description AS {nameof(EventResponse.Description)},
                location AS {nameof(EventResponse.Location)},
                starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
             FROM events.events
             WHERE id = @EventId
             """;

        EventResponse eventResponse = await connection.QuerySingleOrDefaultAsync(sql, request);

        return eventResponse;
    }
}
