using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.Register;
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
        #region UserDependencyInjection
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IForgotPasswordSendMailOnlyRepository, ForgotPasswordRepository>()
                .AddScoped<IRecoveryCodeWriteOnlyRepository, RecoveryCodeRepository>()
                .AddScoped<IRecoveryCodeReadOnlyRepository, RecoveryCodeRepository>();
        #endregion

        #region MaterialDependencyInjection
        services.AddScoped<IMaterialWriteOnlyRepository, MaterialRepository>()
                .AddScoped<IMaterialReadOnlyRepository, MaterialRepository>();
        #endregion

        services.AddScoped<ICollaboratorWriteOnlyRepository, CollaboratorRepository>()
                .AddScoped<ICollaboratorReadOnlyRepository, CollaboratorRepository>();

        services.AddScoped<IBorrowedMaterialWriteOnly, BorrowedRepository>()
                .AddScoped<IBorrowedMaterialReadOnly, BorrowedRepository>();


        services.AddScoped<IMaterialsForCollaboratorWriteOnly, MaterialsForCollaboratorRepository>();
    }
}
