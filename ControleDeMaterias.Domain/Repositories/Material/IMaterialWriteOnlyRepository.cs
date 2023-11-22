namespace ControleDeMateriais.Domain.Repositories.Material;
public interface IMaterialWriteOnlyRepository
{
    Task Register(Entities.Material material);
}
