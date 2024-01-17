using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
using ControleDeMateriais.Application.UseCases.Collaborator.ConfirmPassword;
using ControleDeMateriais.Application.UseCases.Collaborator.Delete;
using ControleDeMateriais.Application.UseCases.Collaborator.Recover;
using ControleDeMateriais.Application.UseCases.Collaborator.Register;
using ControleDeMateriais.Application.UseCases.Collaborator.Update;
using ControleDeMateriais.Application.UseCases.Loan.Confirm;
using ControleDeMateriais.Application.UseCases.Loan.Delete;
using ControleDeMateriais.Application.UseCases.Loan.Devolution;
using ControleDeMateriais.Application.UseCases.Loan.Recover;
using ControleDeMateriais.Application.UseCases.Loan.Selection;
using ControleDeMateriais.Application.UseCases.Login.Login;
using ControleDeMateriais.Application.UseCases.Material.Delete;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Application.UseCases.Material.Register;
using ControleDeMateriais.Application.UseCases.Material.Update;
using ControleDeMateriais.Application.UseCases.User.ForgotPassword;
using ControleDeMateriais.Application.UseCases.User.NewPassword;
using ControleDeMateriais.Application.UseCases.User.Recover;
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
        AddAdditionalKeyPassword(services, configuration);
        AddTokenJwt(services, configuration);
        AddLoggedUser(services, configuration);
    }

    private static void AddLoggedUser(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
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
        #region UserDependencyInjection
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<INewPasswordUseCase, NewPasswordUseCase>();
        services.AddScoped<IRecoverUserUseCase, RecoverUserUseCase>();
        #endregion

        #region MaterialDependencyInjection
        services.AddScoped<IRegisterMaterialUseCase, RegisterMaterialUseCase>();
        services.AddScoped<IRecoverMaterialUseCase, RecoverMaterialUseCase>();
        services.AddScoped<IUpdateMaterialUseCase, UpdateMaterialUseCase>();
        services.AddScoped<IDeleteMaterialUseCase, DeleteMaterialUseCase>();
        #endregion

        #region CollaboratorDependencyInjection
        services.AddScoped<IRegisterCollaboratorUseCase, RegisterCollaboratorUseCase>();
        services.AddScoped<IRecoverCollaboratorUseCase, RecoverCollaboratorUseCase>();
        services.AddScoped<IUpdateCollaboratorUseCase, UpdateCollaboratorUseCase>();
        services.AddScoped<IDeleteCollaboratorUseCase, DeleteCollaboratorUseCase>();
        services.AddScoped<IConfirmPasswordUseCase, ConfirmPasswordUseCase>();
        #endregion

        #region MaterialsLoan
        services.AddScoped<IMaterialSelectionUseCase, MaterialSelectionUseCase>();        
        services.AddScoped<IRecoverMaterialForCollaboratorUseCase, RecoverMaterialForCollaboratorUseCase>();
        services.AddScoped<IConfirmSelectedMaterialUseCase, ConfirmSelectedMaterialUseCase>();
        services.AddScoped<IMaterialDevolutionUseCase, MaterialDevolutionUseCase>();
        services.AddScoped<IDeleteLoanUseCase, DeleteLoanUseCase>();
        #endregion

        #region BorrowedMaterials
        services.AddScoped<IRecoverBorrowedMaterialsUseCase, RecoverBorrowedMaterialsUseCase>();
        #endregion
    }
}
