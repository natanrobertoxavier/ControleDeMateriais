using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public class RecoverCollaboratorValidator : AbstractValidator<string>
{
    public RecoverCollaboratorValidator()
    {
        When(c => !string.IsNullOrEmpty(c), () =>
        {
            RuleFor(c => c).Custom((enrollment, context) =>
            {
                string cpfPattern = "[0-9]{3}.[0-9]{3}-[0-9]{1}";
                var isMatch = Regex.IsMatch(enrollment, cpfPattern);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results
                        .ValidationFailure(nameof(enrollment), ErrorMessagesResource.MATRICULA_COLABORADOR_INVALIDO));
                }
            });
        });
    }
}

