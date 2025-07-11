﻿using System.Reflection;
using Evently.Common.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Evently.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);
        
        return services;
    } 
}
