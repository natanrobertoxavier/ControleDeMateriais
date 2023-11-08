using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Application.UseCases.User.Register;
using ControleDeMateriais.Infrastructure.AccessRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        AddAdditionalKeyPassword(services, configuration);
        AddTokenJwt(services, configuration);
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKeyPassword = Environment.GetEnvironmentVariable("AdditionalKeyPassword");

        services.AddScoped(option => new PasswordEncryptor(additionalKeyPassword));
    }

    private static void AddTokenJwt(IServiceCollection services, IConfiguration configuration)
    {
        var lifeTimeToken = Environment.GetEnvironmentVariable("LifetimeToken");
        var tokenKey = Environment.GetEnvironmentVariable("TokenKey");

        services.AddScoped(option => new TokenController(double.Parse(lifeTimeToken), tokenKey));
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
