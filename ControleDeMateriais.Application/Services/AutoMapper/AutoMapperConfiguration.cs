using AutoMapper;
using MongoDB.Bson;

namespace ControleDeMateriais.Application.Services.AutoMapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        RequestToEntity();
        EntityToResponse();
        ResponseToEntity();
        EntityToLog();
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
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => origin.Id.ToString()))
            .ForMember(destiny => destiny.UserId, config => config.MapFrom(origin => origin.UserId.ToString()));
    }

    private void ResponseToEntity()
    {
        CreateMap<Communication.Responses.ResponseMaterialJson, Domain.Entities.Material>()
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => new ObjectId(origin.Id)))
            .ForMember(destiny => destiny.UserId, config => config.MapFrom(origin => new ObjectId(origin.UserId)));
    }

    private void EntityToLog()
    {
        CreateMap<Domain.Entities.Material, Domain.Entities.MaterialDeletionLog>()
            .ForMember(destiny => destiny.UserIdCreated, config => config.MapFrom(origin => origin.UserId));
    }
}
