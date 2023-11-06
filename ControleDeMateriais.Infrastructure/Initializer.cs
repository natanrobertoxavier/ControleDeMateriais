using ControleDeMateriais.Domain.Repositories;
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
    }
}
