using Evently.Common.Domain;
using Evently.Common.Presentation.Endpoints;
using Evently.Common.Presentation.Results;
using Evently.Modules.Events.Application.Categories.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class UpdateCategory:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{id}", async (Guid id, UpdateCategoryRequest updateCategoryRequest, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateCategoryCommand(id, updateCategoryRequest.Name));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
    }

    internal sealed class UpdateCategoryRequest
    {
        public string Name { get; init; }
    }
}
