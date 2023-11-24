namespace ControleDeMateriais.Domain.Repositories.Material;
public interface IMaterialWriteOnlyRepository
{
    Task Register(Entities.Material material);
    Task<Entities.Material> Update(string id, Entities.Material material);
}
