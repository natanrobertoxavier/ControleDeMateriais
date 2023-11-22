namespace ControleDeMateriais.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> IsThereUserWithEmail(string email);
    Task<Entities.User> IsThereUserWithEmailReturnUser(string email);
    Task<bool> IsThereUserWithCpf(string cpf);
    Task<Entities.User> RecoverEmailPassword(string email, string password);
    Task<Entities.User> RecoverByEmail(string email);
}
