using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Collaborator.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class CollaboratorController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCollaboratorCreatedJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterCollaboratorUseCase useCase,
        [FromBody] RequestCollaboratorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
