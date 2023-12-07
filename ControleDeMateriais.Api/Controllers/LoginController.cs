using ControleDeMateriais.Application.UseCases.Login.Login;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

public class LoginController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
