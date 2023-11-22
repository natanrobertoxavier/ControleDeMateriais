using ControleDeMateriais.Communication.Enum;
using MongoDB.Bson;

namespace ControleDeMateriais.Communication.Responses;
public class ResponseRegisterMaterialJson
{
    public string _id { get; set; }
    public DateTime Created { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public Category Category { get; set; }
}
