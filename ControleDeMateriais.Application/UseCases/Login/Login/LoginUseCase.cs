using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Login.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;

    public LoginUseCase(
        IUserReadOnlyRepository userReadOnlyRepository, 
        PasswordEncryptor passwordEncryptor, 
        TokenController tokenController)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }

    public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.RecoverEmailPassword(request.Email, encryptedPassword) ?? 
            throw new ExceptionLoginInvalid();

        return new ResponseLoginJson
        {
            Name = user.Name,
            Token = _tokenController.TokenGenerate(user.Email)
        };
    }
}
