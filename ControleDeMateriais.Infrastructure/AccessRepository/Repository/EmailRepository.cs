using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Repositories.Email;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class EmailRepository : IEmailSendOnlyRepository
{
    private readonly string AddressApiEmail = Environment.GetEnvironmentVariable("AddressSendMail");

    public async Task SendMail(string content, string subject, string recipient)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var emailSender = new RequestSendMailJson()
        {
            Body = content,
            IsHtml = true,
            Subject = subject,
            Recipient = new List<string>
            {
                recipient
            }
        };

        var dados = JsonConvert.SerializeObject(emailSender);

        var response = await client.PostAsync(AddressApiEmail, new StringContent(dados, Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            throw new ControleDeMateriaisException(ErrorMessagesResource.ERRO_AO_ENVIAR_EMAIL);
        }
    }
}
