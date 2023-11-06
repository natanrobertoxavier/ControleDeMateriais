namespace ControleDeMateriais.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> Messages { get; set; }

    public ResponseErrorJson(string messages)
    {
        Messages = new List<string>
        {
            messages
        };
    }

    public ResponseErrorJson(List<string> messages)
    {
        Messages = messages;
    }
}
