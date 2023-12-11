using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.Register;
public interface IMaterialsForCollaboratorWriteOnly
{
    Task Register(MaterialsForCollaborator materialsForCollaborator);
}
