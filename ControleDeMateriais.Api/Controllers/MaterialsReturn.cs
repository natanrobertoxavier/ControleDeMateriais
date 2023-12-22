using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Devolution;
using ControleDeMateriais.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMateriais.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class MaterialsReturn : ControleDeMateriaisController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MaterialSelection(
        [FromServices] IMaterialDevolutionUseCase useCase,
        [FromBody] RequestMaterialDevolutionJson request)
    {
        await useCase.Execute(request);

        return Ok();
    }
}
