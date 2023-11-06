using ControleDeMateriais.Application.Services.Cryptography;
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
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var section = Environment.GetEnvironmentVariable("AdditionalKeyPassword");
        services.AddScoped(option => new PasswordEncryptor(section));
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
