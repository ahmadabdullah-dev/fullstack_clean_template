using Microsoft.Extensions.DependencyInjection;
using Application.Features.Auth.Validators;
using FluentValidation;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

        return services;
    }
}