using ControleDeMateriais.Communication.Enum;
using MongoDB.Bson;

namespace ControleDeMateriais.Communication.Responses;
public class ResponseMaterialJson
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public Category Category { get; set; }
    public string CategoryDescription { get; set; }
    public string UserId { get; set; }
}
