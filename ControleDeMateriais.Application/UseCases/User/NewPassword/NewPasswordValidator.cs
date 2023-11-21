using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.User.NewPassword;
public class NewPasswordValidator : AbstractValidator<RequestNewPasswordJson>
{
    public NewPasswordValidator()
    {
        RuleFor(c => c.NewPassword).SetValidator(new User.NewPasswordValidator());
        RuleFor(x => x.NewPassword)
            .Equal(x => x.ConfirmPassword)
            .WithMessage(ErrorMessagesResource.SENHAS_NAO_CONFEREM);
    }
}
