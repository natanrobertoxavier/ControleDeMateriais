namespace ControleDeMateriais.Communication.Requests;
public class RequestMaterialDevolutionJson
{
    public string HashId { get; set; }
    public List<string> BarCode { get; set; }
}
