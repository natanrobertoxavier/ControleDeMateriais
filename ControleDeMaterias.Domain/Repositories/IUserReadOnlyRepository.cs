namespace ControleDeMateriais.Domain.Repositories;
public interface IUserReadOnlyRepository
{
    Task<bool> IsThereUserWithEmail(string email);
    Task<bool> IsThereUserWithCpf(string cpf);
    Task<Entities.User> GetEmailPassword(string email, string password);
}
