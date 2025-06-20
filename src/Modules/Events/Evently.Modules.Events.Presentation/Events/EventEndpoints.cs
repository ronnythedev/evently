using Microsoft.AspNetCore.Routing;
namespace Evently.Modules.Events.Presentation.Events;

public static class EventEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
        GetEvents.MapEndpoint(app);
        CancelEvent.MapEndpoint(app);
        RescheduleEvent.MapEndpoint(app);
        SearchEvents.MapEndpoint(app);
        PublishEvent.MapEndpoint(app);
    }
}
