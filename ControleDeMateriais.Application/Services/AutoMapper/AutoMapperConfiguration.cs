using AutoMapper;

namespace ControleDeMateriais.Application.Services.AutoMapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Requests.RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Requests.RequestRegisterMaterialJson, Domain.Entities.Material>();
        CreateMap<Communication.Requests.RequestUpdateMaterialJson, Domain.Entities.Material>();
    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.Material, Communication.Responses.ResponseMaterialJson>()
            .ForMember(destiny => destiny._id, config => config.MapFrom(origin => origin._id.ToString()));
    }
}
