using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleDeMateriais.Domain.Entities;
public class BaseEntity
{
    [BsonId]
    public ObjectId _id {  get; set; }
    public int Id { get; set; }
    public DateTime Created {  get; set; }
}
