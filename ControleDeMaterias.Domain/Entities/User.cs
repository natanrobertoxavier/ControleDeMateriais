using MongoDB.Bson.Serialization.Attributes;

namespace ControleDeMateriais.Domain.Entities;

[BsonIgnoreExtraElements]
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
}
