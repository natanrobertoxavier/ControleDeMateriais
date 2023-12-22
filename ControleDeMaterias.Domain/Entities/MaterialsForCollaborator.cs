using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class MaterialsForCollaborator : BaseEntity
{
    public ObjectId CollaboratorId { get; set; }
    public string MaterialsHashId { get; set; }
    public ObjectId UserId { get; set; }
    public ObjectId CollaboratorConfirmedId { get; set; }
    public bool Confirmed { get; set; } = false;
    public DateTime DateTimeConfirmation { get; set; }
}
