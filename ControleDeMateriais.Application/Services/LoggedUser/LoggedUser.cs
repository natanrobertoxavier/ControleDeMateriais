using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.User;
using Microsoft.AspNetCore.Http;

namespace ControleDeMateriais.Application.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repositoryUserReadOnly;

    public LoggedUser(
        IHttpContextAccessor httpContextAccessor, 
        TokenController tokenController, 
        IUserReadOnlyRepository repositoryUserReadOnly)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repositoryUserReadOnly = repositoryUserReadOnly;
    }

    public async Task<User> RecoveryUser()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var email = _tokenController.RecoverEmail(token);

        var user = await _repositoryUserReadOnly.RecoverByEmail(email);

        return user;
    }
}
