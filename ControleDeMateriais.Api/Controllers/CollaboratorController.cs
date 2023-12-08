using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Collaborator.Recover;
using ControleDeMateriais.Application.UseCases.Collaborator.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class CollaboratorController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCollaboratorCreatedJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterCollaboratorUseCase useCase,
        [FromBody] RequestCollaboratorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseCollaboratorJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverCollaboratorUserCase useCase)
    {
        var result = await useCase.Execute();

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("enrollment/{enrollment}")]
    [ProducesResponseType(typeof(ResponseCollaboratorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverByEnrollment(
        [FromServices] IRecoverCollaboratorUserCase useCase,
        [FromRoute] [Required] string enrollment)
    {
        var result = await useCase.Execute(enrollment);

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }
}
