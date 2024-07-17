using AutoMapper;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Entities;

namespace ProjectFinance.API.MappingProfiles;

public class RequestToDoamin : Profile
{
    public RequestToDoamin()
    {
        CreateMap<CommonCreateRequest, Bank>();
    }
    
}