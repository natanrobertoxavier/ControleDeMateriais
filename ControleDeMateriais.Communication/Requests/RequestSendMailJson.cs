namespace ControleDeMateriais.Communication.Requests;
public class RequestSendMailJson
{
    public List<string> Recipient { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsHtml { get; set; }
}
