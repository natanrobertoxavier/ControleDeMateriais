using ControleDeMateriais.Application.UseCases.Login.Login;
using ControleDeMateriais.Application.UseCases.User.ForgotPassword;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

public class ForgotPasswordController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseForgotPasswordJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword(
        [FromServices] IForgotPasswordUseCase useCase,
        [FromBody] RequestForgotPasswordJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
