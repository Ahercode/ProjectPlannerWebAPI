using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Entities;

namespace ProjectFinance.API.MappingProfiles;

public class DomainToResponse : Profile
    
{
    public DomainToResponse()
    {
        CreateMap<Bank, CommonResponse>();
    }
}