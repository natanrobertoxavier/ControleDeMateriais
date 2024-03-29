﻿using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Entities;
public class CollaboratorDeletionLog : BaseEntity
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Enrollment { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public ObjectId UserIdCreated { get; set; }
    public ObjectId UserIdDeleted { get; set; }
    public DateTime DateDeletion { get; set; } = DateTime.UtcNow;
}
