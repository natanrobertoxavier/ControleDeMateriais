﻿namespace ControleDeMateriais.Domain.Repositories.Material;
public interface IMaterialReadOnlyRepository
{
    Task<List<Entities.Material>> RecoverAll();
    Task<Entities.Material> RecoverById(string Id);
    Task<Entities.Material> RecoverByBarCode(string barCode);
}
