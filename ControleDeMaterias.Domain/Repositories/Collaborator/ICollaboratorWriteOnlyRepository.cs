namespace ControleDeMateriais.Domain.Repositories.Collaborator;
public interface ICollaboratorWriteOnlyRepository
{
    Task Add(Entities.Collaborator collaborator);
    Task<Entities.Collaborator> Update(string enrollment, Entities.Collaborator collaborator);
}
