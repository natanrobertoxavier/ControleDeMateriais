using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class MaterialDevolution
{
    public string HashId { get; set; }
    public List<string> BarCode { get; set; }
    public ObjectId UserReceived { get; set; }
    public DateTime DateReceived { get; set; }
}
