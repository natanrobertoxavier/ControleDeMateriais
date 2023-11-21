using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class RecoveryCode : BaseEntity
{
    public ObjectId UserId { get; set; }
    public string Code { get; set; }
    public bool Active { get; set; }
}
