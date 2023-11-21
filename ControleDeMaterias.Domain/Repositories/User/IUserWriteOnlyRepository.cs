﻿namespace ControleDeMateriais.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task Add(Entities.User user);
    Task UpdatePassword(Entities.User user);
}
