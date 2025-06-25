﻿using Evently.Common.Domain;
using Evently.Modules.Events.Application.Categories.UpdateCategory;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;

internal static class UpdateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{id}", async (Guid id, UpdateCategoryRequest updateCategoryRequest, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateCategoryCommand(id, updateCategoryRequest.Name));

                return result.Match(() => Results.Ok(), ApiResults.ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
    }

    internal sealed class UpdateCategoryRequest
    {
        public string Name { get; init; }
    }
}
