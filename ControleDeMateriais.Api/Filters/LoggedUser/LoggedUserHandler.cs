using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Domain.Repositories.User;
using Microsoft.AspNetCore.Authorization;

namespace ControleDeMateriais.Api.Filters.LoggedUser;

public class LoggedUserHandler : AuthorizationHandler<LoggedUserRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repositoryUserReadOnly;

    public LoggedUserHandler(
        IHttpContextAccessor httpContextAccessor, 
        TokenController tokenController, 
        IUserReadOnlyRepository repositoryUserReadOnly)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repositoryUserReadOnly = repositoryUserReadOnly;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        LoggedUserRequirement requirement)
    {
        try
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();

            var email = _tokenController.RecoverEmail(token);

            var usuario = await _repositoryUserReadOnly.RecoverByEmail(email);

            if (usuario is null)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
        }
    }
}
