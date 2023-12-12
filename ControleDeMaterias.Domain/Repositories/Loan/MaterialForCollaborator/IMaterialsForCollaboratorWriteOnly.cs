using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
public interface IMaterialsForCollaboratorWriteOnly
{
    Task Add(MaterialsForCollaborator materialsForCollaborator);
    Task Confirm(string hashId, string userId);
}
