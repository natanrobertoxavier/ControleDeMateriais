using MongoDB.Bson;

namespace ControleDeMateriais.Communication.Requests;
public class RequestMaterialSelectionJson
{
    public string Enrollment { get; set; }
    public List<string> BarCode {  get; set; }
}
