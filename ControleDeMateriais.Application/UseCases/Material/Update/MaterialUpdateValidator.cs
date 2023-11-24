using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.Material.Update;
public class MaterialUpdateValidator : AbstractValidator<RequestUpdateMaterialJson>
{
    public MaterialUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessagesResource.NOME_MATERIAL_EM_BRANCO);
        RuleFor(x => x.Description).NotEmpty().WithMessage(ErrorMessagesResource.DESCRICAO_EM_BRANCO);
        RuleFor(x => x.Category).IsInEnum().WithMessage(ErrorMessagesResource.CATEGORIA_INVALIDA);
    }
}
