using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.User.NewPassword;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class NewPasswordController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseNewPasswordJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> NewPassword(
        [FromServices] INewPasswordUseCase useCase,
        [FromBody] RequestNewPasswordJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
