using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.Material.Register;
public class MaterialValidator : AbstractValidator<RequestRegisterMaterialJson>
{
    public MaterialValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessagesResource.DESCRICAO_EM_BRANCO);
        RuleFor(x => x.Category).IsInEnum().WithMessage(ErrorMessagesResource.CATEGORIA_INVALIDA);
        RuleFor(x => x.BarCode)
                    .NotEmpty().WithMessage(ErrorMessagesResource.CODIGO_BARRAS_EM_BRANCO)
                    .Must(ValidateBarCode).WithMessage(ErrorMessagesResource.CODIGO_BARRAS_TAMANHO_INVALIDO)
                    .Matches("^[0-9]+$").WithMessage(ErrorMessagesResource.CODIGO_BARRAS_INVALIDO); ;
    }

    private bool ValidateBarCode(string barCode)
    {
        return barCode.Length == 8 || barCode.Length == 13;
    }
}
