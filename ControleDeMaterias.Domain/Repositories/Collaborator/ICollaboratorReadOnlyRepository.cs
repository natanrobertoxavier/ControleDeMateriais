namespace ControleDeMateriais.Domain.Repositories.Collaborator;
public interface ICollaboratorReadOnlyRepository
{
    Task<List<Entities.Collaborator>> RecoverAll();
    Task<Entities.Collaborator> RecoverByEnrollment(string enrollment);
    Task<Entities.Collaborator> ConfirmPassword(string enrollment, string password);
    Task<bool> IsThereUserWithEmail(string email);
    Task<bool> IsThereUserWithEnrollment(string enrollment);
}
