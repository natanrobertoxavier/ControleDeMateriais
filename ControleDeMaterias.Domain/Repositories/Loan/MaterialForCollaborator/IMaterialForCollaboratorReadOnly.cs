using ControleDeMateriais.Domain.Entities;
using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
public interface IMaterialForCollaboratorReadOnly
{
    Task<List<MaterialsForCollaborator>> RecoverAll();
    Task<List<MaterialsForCollaborator>> RecoverByCollaborator(ObjectId id);
    Task<MaterialsForCollaborator> RecoverByHashId(string hashId);
    Task<List<MaterialsForCollaborator>> RecoverByHashIds(List<string> hashId);
}
