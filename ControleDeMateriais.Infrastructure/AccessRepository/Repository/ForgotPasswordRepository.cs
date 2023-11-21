using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class ForgotPasswordRepository : IForgotPasswordSendMailOnlyRepository
{
    private readonly string AddressApiEmail = Environment.GetEnvironmentVariable("AddressSendMail");

    public async Task SendMail(User user, string recoveryCode)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var emailSender = new RequestSendMailJson()
        {
            Body = InsertBodyEmail(user, recoveryCode),
            IsHtml = true,
            Subject = "Código para recuperação de senha",
            Recipient = new List<string>
            {
                user.Email
            }
        };

        var dados = JsonConvert.SerializeObject(emailSender);

        var response = await client.PostAsync(AddressApiEmail, new StringContent(dados, Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            throw new ControleDeMateriaisException(ErrorMessagesResource.ERRO_AO_ENVIAR_EMAIL);
        }
    }

    private static string InsertBodyEmail(User user, string recoveryCode)
    {
        return string.Concat($"Olá {user.Name}, aqui está seu código para recuperação de senha: {recoveryCode}. Esse código é valido por 30 minutos.");
    }
}
