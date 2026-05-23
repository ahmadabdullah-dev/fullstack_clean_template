using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Application.Features.Todo.Validators;
using Application.Features.User.Validators;
namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)); // register once will apply to all

        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>(); // register once will apply to all


        return services;
    }
}