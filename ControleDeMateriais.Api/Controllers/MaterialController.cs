using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Delete;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Application.UseCases.Material.Register;
using ControleDeMateriais.Application.UseCases.Material.Update;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

    [HttpPut]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateMaterialUseCase useCase,
        [FromHeader] [Required] string id,
        [FromBody] RequestUpdateMaterialJson request)
    {
        var result = await useCase.Execute(id, request);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverMaterialUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("category/{category}")]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverByCategory(
        [FromServices] IRecoverMaterialUseCase useCase,
        [FromRoute] int category)
    {
        var result = await useCase.Execute(category);

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("codebar/{barCode}")]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverByBarCode(
        [FromServices] IRecoverMaterialUseCase useCase,
        [FromRoute] string barCode)
    {
        var result = await useCase.Execute(barCode);

        if (result is not null)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpDelete]
    [Route("codebar/{barCode}")]
    [ProducesResponseType(typeof(ResponseMaterialJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteMaterialUseCase useCase,
        [FromRoute] string barCode)
    {
        await useCase.Execute(barCode);

        return Ok();
    }
}
