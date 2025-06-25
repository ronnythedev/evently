﻿using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace Evently.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (CreateEventRequest createEventRequest, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateEventCommand(
                    createEventRequest.CategoryId,
                    createEventRequest.Title,
                    createEventRequest.Description,
                    createEventRequest.Location,
                    createEventRequest.StartsAtUtc,
                    createEventRequest.EndsAtUtc));

                return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Events);
    }

    internal sealed class CreateEventRequest
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartsAtUtc { get; set; }

        public DateTime? EndsAtUtc { get; set; }
    }
}
