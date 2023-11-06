using ControleDeMateriais.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeMateriais.Application;
public static class Initializer
{
    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddUseCase(services);
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
