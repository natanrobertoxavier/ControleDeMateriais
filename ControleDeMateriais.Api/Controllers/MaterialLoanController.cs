using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Loan.Confirm;
using ControleDeMateriais.Application.UseCases.Loan.Devolution;
using ControleDeMateriais.Application.UseCases.Loan.Selection;
using ControleDeMateriais.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class MaterialLoanController : ControleDeMateriaisController
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

    [HttpPut]
    [Route("confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmSelectedMaterial(
        [FromServices] IConfirmSelectedMaterialUseCase useCase,
        [FromBody] RequestConfirmSelectedMaterialJson request)
    {
        await useCase.Execute(request);

        return Ok();
    }
}
