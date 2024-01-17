using ControleDeMateriais.Domain.Enum;
using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;

public class MaterialDeletionLog : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public Category Category { get; set; }
    public ObjectId UserIdCreated { get; set; }
    public ObjectId UserIdDeleted { get; set; }
    public DateTime DateDeletion { get; set; } = DateTime.UtcNow;
}

