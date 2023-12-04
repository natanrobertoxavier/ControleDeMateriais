﻿using AutoMapper;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Material.Recover;
public class RecoverMaterialUseCase : IRecoverMaterialUseCase
{
    private readonly IMapper _mapper;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnly;

    public RecoverMaterialUseCase(IMapper mapper, IMaterialReadOnlyRepository repositoryMaterialReadOnly)
    {
        _mapper = mapper;
        _repositoryMaterialReadOnly = repositoryMaterialReadOnly;
    }

    public async Task<List<ResponseMaterialJson>> Execute()
    {
        var material = await _repositoryMaterialReadOnly.RecoverAll();

        return _mapper.Map<List<ResponseMaterialJson>>(material);
    }

    public async Task<List<ResponseMaterialJson>> Execute(int category)
    {
        var material = await _repositoryMaterialReadOnly.RecoverByCategory(category);

        return _mapper.Map<List<ResponseMaterialJson>>(material);
    }

    public async Task<ResponseMaterialJson> Execute(string codeBar)
    {
        ValidateData(codeBar);

        var material = await _repositoryMaterialReadOnly.RecoverByBarCode(codeBar);

        return _mapper.Map<ResponseMaterialJson>(material);
    }

    private static void ValidateData(string codeBar)
    {
        var validator = new RecoverMaterialValidator();
        var result = validator.Validate(codeBar);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
