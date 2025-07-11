﻿using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

internal static class RescheduleEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("events/{id}/reschedule", async (Guid id, RescheduleEventRequest rescheduleEventRequest, ISender sender) =>
            {
                Result result = await sender.Send(
                    new RescheduleEventCommand(id, rescheduleEventRequest.StartsAtUtc, rescheduleEventRequest.EndsAtUtc));

                return result.Match(Results.NoContent, ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Events);
    }

    internal sealed class RescheduleEventRequest
    {
        public DateTime StartsAtUtc { get; init; }

        public DateTime? EndsAtUtc { get; init; }
    }
}
