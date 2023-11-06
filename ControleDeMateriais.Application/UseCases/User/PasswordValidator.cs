using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.User;
public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ErrorMessagesResource.SENHA_USUARIO_EM_BRANCO);
        When(password => !string.IsNullOrEmpty(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ErrorMessagesResource.SENHA_MINIMO_SEIS_CARACTERES);
        });
    }
}
