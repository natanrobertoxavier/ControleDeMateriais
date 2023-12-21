using ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
using ControleDeMateriais.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

public class MaterialsReturn : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MaterialSelection(
        [FromServices] IMaterialSelectionUseCase useCase,
        [FromBody] RequestMaterialSelectionJson request)
    {
        await useCase.Execute(request);

        return Created(string.Empty, null);
    }
}
