using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Loan.Recover;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class BorrowedMaterialsController : ControleDeMateriaisController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseMaterialForCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverMaterialForCollaboratorUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }
}
