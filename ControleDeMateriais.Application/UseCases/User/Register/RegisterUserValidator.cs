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
        RuleFor(c => c.Cpf).NotEmpty().WithMessage(ErrorMessagesResource.CPF_USUARIO_EM_BRANCO);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ErrorMessagesResource.EMAIL_USUARIO_INVALIDO);
        });
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email).Custom((email, context) =>
            {
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                var isMatch = Regex.IsMatch(email, pattern);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results
                        .ValidationFailure(nameof(email), ErrorMessagesResource.EMAIL_USUARIO_INVALIDO));
                }
            });
        });
        When(c => !string.IsNullOrEmpty(c.Cpf), () =>
        {
            RuleFor(c => c.Cpf).Custom((cpf, context) => 
            {
                string cpfPattern = "[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}";
                var isMatch = Regex.IsMatch(cpf, cpfPattern);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results
                        .ValidationFailure(nameof(cpf), ErrorMessagesResource.CPF_USUARIO_INVALIDO));
                }
            });
        });
        When(c => !string.IsNullOrEmpty(c.Telephone), () =>
        {
            RuleFor(c => c.Telephone).Custom((telephone, context) => 
            {
                string telephonePattern = "[0-9]{2} [9]{1} [0-9]{4}-[0-9]{4}";
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
