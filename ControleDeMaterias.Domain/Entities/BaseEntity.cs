using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleDeMateriais.Domain.Entities;
public class BaseEntity
{
    [BsonId]
    public ObjectId Id {  get; set; }
    public DateTime Created {  get; set; } = DateTime.UtcNow;
}
