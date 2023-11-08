namespace ControleDeMateriais.Domain.Repositories;
public interface IUserReadOnlyRepository
{
    Task<bool> IsThereUserWithEmail(string email);
}
