using ControleDeMateriais.Application.UseCases.Login.Login;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

public class MaterialsLoan : ControleDeMateriaisController
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
