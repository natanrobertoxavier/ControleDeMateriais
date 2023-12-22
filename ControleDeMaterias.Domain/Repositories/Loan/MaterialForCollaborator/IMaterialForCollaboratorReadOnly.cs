using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
public interface IMaterialForCollaboratorReadOnly
{
    Task<List<MaterialsForCollaborator>> RecoverAll();
    Task<MaterialsForCollaborator> RecoverByHashId(string hashId);
    Task<List<MaterialsForCollaborator>> RecoverByHashIds(List<string> hashId);
}
