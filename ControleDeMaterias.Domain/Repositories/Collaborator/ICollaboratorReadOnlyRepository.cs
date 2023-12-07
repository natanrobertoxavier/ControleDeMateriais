namespace ControleDeMateriais.Domain.Repositories.Collaborator;
public interface ICollaboratorReadOnlyRepository
{
    Task<bool> IsThereUserWithEmail(string email);
    Task<bool> IsThereUserWithEnrollment(string enrollment);
}
