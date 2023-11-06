using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMateriais.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(ErrorMessagesResource.NOME_USUARIO_EM_BRANCO);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ErrorMessagesResource.EMAIL_USUARIO_EM_BRANCO);
        RuleFor(c => c.Telephone).NotEmpty().WithMessage(ErrorMessagesResource.TELEFONE_USUARIO_EM_BRANCO);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ErrorMessagesResource.EMAIL_USUARIO_INVALIDO);
        });
        When(c => !string.IsNullOrEmpty(c.Telephone), () =>
        {
            RuleFor(c => c.Telephone).Custom((telephone, context) => 
            {
                string telephonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telephone, telephonePattern);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results
                        .ValidationFailure(nameof(telephone), ErrorMessagesResource.TELEFONE_USUARIO_INVALIDO));
                }
            });
        });
    }
}
