using ControleDeMateriais.Domain.Entities;
using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
public interface IMaterialForCollaboratorReadOnly
{
    Task<List<MaterialsForCollaborator>> RecoverAll();
    Task<List<MaterialsForCollaborator>> RecoverByCollaborator(ObjectId id);
    Task<List<MaterialsForCollaborator>> RecoverByCollaboratorLoanStatus(ObjectId id, bool status);
    Task<List<MaterialsForCollaborator>> RecoverByStatus(bool status);
    Task<MaterialsForCollaborator> RecoverByHashId(string hashId);
    Task<List<MaterialsForCollaborator>> RecoverByHashIds(List<string> hashId);
}
