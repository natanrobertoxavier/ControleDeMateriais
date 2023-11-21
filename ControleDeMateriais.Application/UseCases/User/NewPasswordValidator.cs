using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.User;
public class NewPasswordValidator : AbstractValidator<string>
{
    public NewPasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ErrorMessagesResource.NOVA_SENHA_EM_BRANCO);
        When(password => !string.IsNullOrEmpty(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ErrorMessagesResource.NOVA_SENHA_SEIS_CARACTERES);
        });
    }
}
