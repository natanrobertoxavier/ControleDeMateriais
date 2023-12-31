﻿using AutoMapper;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.User;
using MongoDB.Bson;

namespace ControleDeMateriais.Application.UseCases.User.Recover;
public class RecoverUserUseCase : IRecoverUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public RecoverUserUseCase(
        IMapper mapper, 
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<ResponseUserJson> Execute(string id)
    {
        var result = await _userReadOnlyRepository.RecoverById(new ObjectId(id));
        return _mapper.Map<ResponseUserJson>(result);
    }
}
