using MongoDB.Bson;

namespace ControleDeMateriais.Communication.Requests;
public class RequestMaterialSelectionJson
{
    public string CollaboratorId { get; set; }
    public List<string> BarCode {  get; set; }
}
