namespace ControleDeMateriais.Domain.Repositories.Collaborator;
public interface ICollaboratorWriteOnlyRepository
{
    Task Add(Entities.Collaborator collaborator);
}
