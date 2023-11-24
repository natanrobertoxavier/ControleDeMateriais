using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Application.UseCases.Material.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class MaterialController : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterMaterialUseCase useCase,
        [FromBody] RequestRegisterMaterialJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("{codeBar}")]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverBarCode(
        [FromServices] IRecoverMaterialUseCase useCase,
        [FromRoute] string codeBar)
    {
        var result = await useCase.Execute(codeBar);

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }
}
