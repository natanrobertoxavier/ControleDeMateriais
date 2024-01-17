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

        CreateMap<Communication.Requests.RequestCollaboratorJson, Domain.Entities.Collaborator>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Requests.RequestUpdateCollaboratorJson, Domain.Entities.Collaborator>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Requests.RequestMaterialSelectionJson, Domain.Entities.BorrowedMaterial>();
    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.Material, Communication.Responses.ResponseMaterialJson>()
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => origin.Id.ToString()))
            .ForMember(destiny => destiny.UserId, config => config.MapFrom(origin => origin.UserId.ToString()));

        CreateMap<Domain.Entities.Collaborator, Communication.Responses.ResponseCollaboratorJson>()
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => origin.Id.ToString()))
            .ForMember(destiny => destiny.UserIdCreated, config => config.MapFrom(origin => origin.UserIdCreated.ToString()));

        CreateMap<Domain.Entities.User, Communication.Responses.ResponseUserJson>();

        CreateMap<Domain.Entities.BorrowedMaterial, Communication.Responses.ResponseBorrowedMaterialJson>();
    }

    private void ResponseToEntity()
    {
        CreateMap<Communication.Responses.ResponseMaterialJson, Domain.Entities.Material>()
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => new ObjectId(origin.Id)))
            .ForMember(destiny => destiny.UserId, config => config.MapFrom(origin => new ObjectId(origin.UserId)));

        CreateMap<Communication.Responses.ResponseCollaboratorJson, Domain.Entities.Collaborator>()
            .ForMember(destiny => destiny.Id, config => config.MapFrom(origin => new ObjectId(origin.Id)))
            .ForMember(destiny => destiny.UserIdCreated, config => config.MapFrom(origin => new ObjectId(origin.UserIdCreated)))
            .ForMember(destiny => destiny.Password, config => config.Ignore());
    }

    private void EntityToLog()
    {
        CreateMap<Domain.Entities.Material, Domain.Entities.MaterialDeletionLog>()
            .ForMember(destiny => destiny.UserIdCreated, config => config.MapFrom(origin => origin.UserId));

        CreateMap<Domain.Entities.Collaborator, Domain.Entities.CollaboratorDeletionLog>();

        CreateMap<Domain.Entities.BorrowedMaterial, Domain.Entities.BorrowedMaterialDeletionLog>();

        CreateMap<Domain.Entities.MaterialsForCollaborator, Domain.Entities.MaterialsForCollaboratorDeletionLog>()
            .ForMember(destiny => destiny.UserIdCreated, config => config.MapFrom(origin => origin.UserId));
    }
}
