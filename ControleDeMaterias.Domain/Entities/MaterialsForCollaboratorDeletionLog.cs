using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class MaterialsForCollaboratorDeletionLog : BaseEntity
{
    public ObjectId CollaboratorId { get; set; }
    public string MaterialsHashId { get; set; }
    public ObjectId UserIdCreated { get; set; }
    public DateTime DateDeletion { get; set; } = DateTime.UtcNow;
    public ObjectId UserIdDeleted { get; set; }
}
