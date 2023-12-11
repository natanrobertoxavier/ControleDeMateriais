namespace ControleDeMateriais.Domain.Repositories.Material;
public interface IMaterialWriteOnlyRepository
{
    Task Add(Entities.Material material);
    Task RegisterDeletionLog(Entities.MaterialDeletionLog material);
    Task<Entities.Material> Update(string id, Entities.Material material);
    Task Delete(string codeBar);
}
