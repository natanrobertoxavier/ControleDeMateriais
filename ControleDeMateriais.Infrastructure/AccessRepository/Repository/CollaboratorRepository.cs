using ControleDeMateriais.Communication.Responses;
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

    public async Task<Collaborator> Update(string enrollment, Collaborator collaborator)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var filter = Builders<Collaborator>.Filter.Where(c => c.Enrollment.Equals(enrollment));

        var updateObject = Builders<Collaborator>.Update
            .Set(c => c.Name, collaborator.Name)
            .Set(c => c.Nickname, collaborator.Nickname)
            .Set(c => c.Email, collaborator.Email)
            .Set(c => c.Telephone, collaborator.Telephone);

        await collection.UpdateOneAsync(filter, updateObject);

        var result = await RecoverByEnrollment(enrollment);

        return result;
    }

    public async Task<List<Collaborator>> RecoverAll()
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var filter = Builders<Collaborator>.Filter.Empty;

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<Collaborator> RecoverByEnrollment(string enrollment)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var filter = Builders<Collaborator>.Filter.Where(c => c.Enrollment.Equals(enrollment));
        var result = await collection.Find(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> IsThereUserWithEnrollment(string enrollment)
    {
        var collection = ConnectDataBase.GetCollaboratorAccess();
        var result = await collection.FindAsync(c => c.Enrollment.Equals(enrollment));

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
