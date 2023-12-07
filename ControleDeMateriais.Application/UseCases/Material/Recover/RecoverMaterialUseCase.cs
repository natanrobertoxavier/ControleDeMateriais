using AutoMapper;
using ControleDeMateriais.Communication.Enum;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
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
        var result = GetCategoryDescription(_mapper.Map<List<ResponseMaterialJson>>(material));

        return result;
    }

    public async Task<List<ResponseMaterialJson>> Execute(int category)
    {
        var material = await _repositoryMaterialReadOnly.RecoverByCategory(category);
        var result = GetCategoryDescription(_mapper.Map<List<ResponseMaterialJson>>(material));

        return result;
    }

    public async Task<ResponseMaterialJson> Execute(string codeBar)
    {
        ValidateData(codeBar);

        var material = await _repositoryMaterialReadOnly.RecoverByBarCode(codeBar);
        var result = _mapper.Map<ResponseMaterialJson>(material);
        result.CategoryDescription = EnumExtensions.GetDescription(result.Category);

        return result;
    }

    private static void ValidateData(string codeBar)
    {
        var validator = new RecoverMaterialValidator();
        var result = validator.Validate(codeBar);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }

    private static List<ResponseMaterialJson> GetCategoryDescription(List<ResponseMaterialJson> responseMaterialJsons)
    {
        foreach (var material in responseMaterialJsons)
        {
            material.CategoryDescription = EnumExtensions.GetDescription(material.Category);
        }

        return responseMaterialJsons;
    }
}
