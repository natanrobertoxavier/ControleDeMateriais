using Amazon.SecurityToken.Model;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ControleDeMateriais.Api.Filters.LoggedUser;

public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserAttribute(
        TokenController tokenController,
        IUserReadOnlyRepository repository)
    {
        _tokenController = tokenController;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenInRequest(context);
            var emailUser = _tokenController.RecoverEmail(token);

            var user = await _repository.RecoverByEmail(emailUser) ??
                throw new ControleDeMateriaisException(ErrorMessagesResource.USUARIO_NAO_LOCALIZADO);
        }
        catch (SecurityTokenException)
        {
            ExpiredToken(context);
        }
        catch
        {
            UserWithoutPermission(context);
        }
    }

    private static string TokenInRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new ControleDeMateriaisException(ErrorMessagesResource.TOKEN_INVALIDO);
        }

        return authorization["Bearer".Length..].Trim();
    }

    private static void ExpiredToken(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ErrorMessagesResource.TOKEN_EXPIRADO));
    }

    private static void UserWithoutPermission(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ErrorMessagesResource.USUARIO_SEM_PREMISSAO));
    }
}
