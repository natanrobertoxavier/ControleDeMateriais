using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class CollaboratorRepository : ICollaboratorWriteOnlyRepository, ICollaboratorReadOnlyRepository
{
    public async Task Add(Collaborator collaborator)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        await collection.InsertOneAsync(collaborator);
    }

    public async Task<bool> IsThereUserWithCpf(string cpf)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var result = await collection.FindAsync(c => c.Cpf.Equals(cpf));

        if (await result.AnyAsync())
            return true;

        return false;
    }

    public async Task<bool> IsThereUserWithEmail(string email)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var result = await collection.FindAsync(c => c.Email.Equals(email));

        if (await result.AnyAsync())
            return true;

        return false;
    }
}
