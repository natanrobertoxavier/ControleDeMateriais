using AutoMapper;

namespace ControleDeMateriais.Application.Services.AutoMapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        EntityToResponse();
        RequestToEntity();
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Requests.RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Requests.RequestRegisterMaterialJson, Domain.Entities.Material>();
    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.Material, Communication.Responses.ResponseRegisterMaterialJson>()
            .ForMember(destiny => destiny._id, config => config.MapFrom(origin => origin._id.ToString()));
    }
}
