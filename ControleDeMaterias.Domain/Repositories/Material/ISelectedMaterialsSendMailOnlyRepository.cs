using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Domain.Repositories.Material;
public interface ISelectedMaterialsSendMailOnlyRepository
{
    Task SendMail(RequestSelectedMaterialsContentEmailJson selectedMaterials, ResponseCollaboratorJson collaborator);
}
