using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class BorrowedMaterial : BaseEntity
{
    public string HashId { get; set; }
    public string BarCode { get; set; }
    public bool Active { get; set; } = false;
    public ObjectId UserReceived {  get; set; }
    public DateTime DateReceived { get; set; }
}
