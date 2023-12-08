using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class CollaborationDeletionLog
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Enrollment { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
    public ObjectId UserId { get; set; }
    public ObjectId UserIdDeleted { get; set; }
}
