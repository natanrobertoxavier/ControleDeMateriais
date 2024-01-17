using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class BorrowedMaterialDeletionLog : BaseEntity
{
    public string HashId { get; set; }
    public string BarCode { get; set; }
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public DateTime DateDeletion { get; set; } = DateTime.UtcNow;
}
