namespace ControleDeMateriais.Domain.Repositories.Material;
public interface IMaterialWriteOnlyRepository
{
    Task Register(Entities.Material material);
    Task RegisterDeletionLog(Entities.MaterialDeletionLog material);
    Task<Entities.Material> Update(string id, Entities.Material material);
    Task Delete(string codeBar);
}
