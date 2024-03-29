using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Loan.Confirm;
using ControleDeMateriais.Application.UseCases.Loan.Delete;
using ControleDeMateriais.Application.UseCases.Loan.Selection;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Exceptions.ExceptionBase;
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

    [HttpDelete]
    [Route("hashId/{hashId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteLoanUseCase useCase,
        [FromRoute] string hashId)
    {
        try
        {
            await useCase.Execute(hashId);

            return Ok();
        }
        catch (ExceptionNoContentErrors)
        {
            return NoContent();
        }
    }
}
