using Microsoft.AspNetCore.Routing;
namespace Evently.Modules.Events.Presentation.TicketTypes;

public static class TicketTypeEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateTicketType.MapEndpoint(app);
    }
}
