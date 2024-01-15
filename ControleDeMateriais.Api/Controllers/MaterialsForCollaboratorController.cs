using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Loan.Recover;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class MaterialsForCollaboratorController : ControleDeMateriaisController
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

    [HttpGet]
    [Route("enrollment/{enrollment}/confirmed/{status}")]
    [ProducesResponseType(typeof(ResponseMaterialForCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByCollaboratorAndStatus(
        [FromServices] IRecoverMaterialForCollaboratorUseCase useCase,
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
    [ProducesResponseType(typeof(ResponseMaterialForCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByStatus(
        [FromServices] IRecoverMaterialForCollaboratorUseCase useCase,
        [FromRoute] bool status)
    {
        var result = await useCase.Execute(status);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("barCode/{barCode}")]
    [ProducesResponseType(typeof(ResponseMaterialForCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByBarCode(
        [FromServices] IRecoverMaterialForCollaboratorUseCase useCase,
        [FromRoute] string barCode)
    {
        var result = await useCase.Execute(barCode);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("date")]
    [ProducesResponseType(typeof(ResponseMaterialForCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoveryByDate(
        [FromServices] IRecoverMaterialForCollaboratorUseCase useCase,
        [FromQuery] DateTime? initial = null,
        [FromQuery] DateTime? final = null)
    {
        var result = await useCase.Execute(initial, final);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }
}
