using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
using ControleDeMateriais.Infrastructure.AccessRepository.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeMateriais.Infrastructure;
public static class Initializer
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configurationManager)
    {
        AddRepository(services);
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IForgotPasswordSendMailOnlyRepository, ForgotPasswordRepository>();
        services.AddScoped<IRecoveryCodeWriteOnlyRepository, RecoveryCodeRepository>();
        services.AddScoped<IRecoveryCodeReadOnlyRepository, RecoveryCodeRepository>();
        services.AddScoped<IMaterialWriteOnlyRepository, MaterialRepository>();
        services.AddScoped<IMaterialReadOnlyRepository, MaterialRepository>();
    }
}
