using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class BorrowedMaterialsController : ControleDeMateriaisController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverBorrowedMaterialsUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("status/{status}/received{received}")]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByStatus(
        [FromServices] IRecoverBorrowedMaterialsUseCase useCase,
        [FromRoute] bool status,
        [FromRoute] bool received)
    {
        var result = await useCase.Execute(status, received);

        return Ok(result);
    }
}
