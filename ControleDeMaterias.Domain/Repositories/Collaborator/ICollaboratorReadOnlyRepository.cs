namespace ControleDeMateriais.Domain.Repositories.Collaborator;
public interface ICollaboratorReadOnlyRepository
{
    Task<List<Domain.Entities.Collaborator>> RecoverAll();
    Task<Domain.Entities.Collaborator> RecoverByEnrollment(string enrollment);
    Task<bool> IsThereUserWithEmail(string email);
    Task<bool> IsThereUserWithEnrollment(string enrollment);
}
