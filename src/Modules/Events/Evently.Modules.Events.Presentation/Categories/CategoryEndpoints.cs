﻿using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;

public static class CategoryEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateCategory.MapEndpoint(app);
        ArchiveCategory.MapEndpoint(app);
        GetCategory.MapEndpoint(app);
        GetCategories.MapEndpoint(app);
        UpdateCategory.MapEndpoint(app);
    }
}
