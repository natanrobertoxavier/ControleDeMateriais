using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
public interface IMaterialForCollaboratorReadOnly
{
    Task<MaterialsForCollaborator> RecoverByHashId(string hashId);
}
