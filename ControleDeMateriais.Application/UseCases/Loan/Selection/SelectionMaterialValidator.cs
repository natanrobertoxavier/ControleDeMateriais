using ControleDeMateriais.Communication.Requests;
using FluentValidation;

namespace ControleDeMateriais.Application.UseCases.Loan.Selection;
public class SelectionMaterialValidator : AbstractValidator<string>
{
    public SelectionMaterialValidator()
    {

    }
}
