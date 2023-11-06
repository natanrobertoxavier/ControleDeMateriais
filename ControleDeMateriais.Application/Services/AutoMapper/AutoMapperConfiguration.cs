using AutoMapper;

namespace ControleDeMateriais.Application.Services.AutoMapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        EntityToResponse();
    }

    private void EntityToResponse()
    {
        CreateMap<Communication.Requests.RequestRegisterUserJson, Domain.Entities.User>();
    }
}
