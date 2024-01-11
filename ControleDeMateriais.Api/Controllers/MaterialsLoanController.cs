using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class MaterialsLoanController : ControleDeMateriaisController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverBorrowedMaterialUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("enrollment/{enrollment}/status/{status}")]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByCollaboratorAndStatus(
        [FromServices] IRecoverBorrowedMaterialUseCase useCase,
        [FromRoute] string enrollment,
        [FromRoute] bool status)
    {
        var result = await useCase.Execute(enrollment, status);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("confirmed/{status}")]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByStatus(
        [FromServices] IRecoverBorrowedMaterialUseCase useCase,
        [FromRoute] bool status)
    {
        var result = await useCase.Execute(status);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("barCode/{barCode}")]
    [ProducesResponseType(typeof(ResponseBorrowedMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByBarCode(
        [FromServices] IRecoverBorrowedMaterialUseCase useCase,
        [FromRoute] string barCode)
    {
        var result = await useCase.Execute(barCode);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

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
