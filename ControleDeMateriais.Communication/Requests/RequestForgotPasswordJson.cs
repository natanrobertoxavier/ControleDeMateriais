namespace ControleDeMateriais.Communication.Requests;
public class RequestForgotPasswordJson
{
    public string Email { get; set; }
    public bool IsHtml { get; set; } = true;
}
