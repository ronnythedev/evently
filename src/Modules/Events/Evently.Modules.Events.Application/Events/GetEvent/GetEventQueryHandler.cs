using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
namespace Evently.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                e.id AS {nameof(EventResponse.Id)},
                e.category_id AS {nameof(EventResponse.CategoryId)},
                e.title AS {nameof(EventResponse.Title)},
                e.description AS {nameof(EventResponse.Description)},
                e.location AS {nameof(EventResponse.Location)},
                e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
             FROM events.events
             WHERE id = @EventId
             """;

        EventResponse eventResponse = await connection.QuerySingleOrDefaultAsync(sql, request);

        return eventResponse;
    }
}
