using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.Material.Recover;
public class RecoverMaterialValidator : AbstractValidator<string>
{
    public RecoverMaterialValidator()
    {
        RuleFor(x => x)
                    .NotEmpty().WithMessage(ErrorMessagesResource.CODIGO_BARRAS_EM_BRANCO)
                    .Must(ValidateBarCode).WithMessage(ErrorMessagesResource.CODIGO_BARRAS_TAMANHO_INVALIDO)
                    .Matches("^[0-9]+$").WithMessage(ErrorMessagesResource.CODIGO_BARRAS_INVALIDO);
    }

    private bool ValidateBarCode(string barCode)
    {
        return barCode.Length == 8 || barCode.Length == 13;
    }
}
