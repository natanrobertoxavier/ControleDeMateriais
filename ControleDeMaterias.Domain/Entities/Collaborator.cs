namespace ControleDeMateriais.Domain.Entities;
public class Collaborator : BaseEntity
{
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Enrollment { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
}
