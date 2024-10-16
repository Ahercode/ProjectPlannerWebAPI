using AutoMapper;
using ProjectFinance.Domain.Dtos.Requests;
using ProjectFinance.Domain.Dtos.Requests.Updates;
using ProjectFinance.Domain.Entities;

namespace ProjectFinance.API.MappingProfiles;

public class RequestToDoamin : Profile
{
    public RequestToDoamin()
    {
        CreateMap<CommonCreateRequest, Bank>().ReverseMap();
        CreateMap<CommonUpdateRequest, Bank>().ReverseMap();
        CreateMap<CommonCreateRequest, Activity>().ReverseMap();
        CreateMap<CommonUpdateRequest, Activity>().ReverseMap();
        CreateMap<CommonFinanceOptionRequest, FinanceOption>().ReverseMap();
        CreateMap<UpdateFinanceOptionRequest, FinanceOption>().ReverseMap();
        CreateMap<CommonFinanceOptionScheduleRequest, FinanceOptionSchedule>().ReverseMap();
        CreateMap<UpdateFinanceOptionScheduleRequest, FinanceOptionSchedule>().ReverseMap();
        CreateMap<CommonInvoiceRequest, Invoice>().ReverseMap();
        CreateMap<UpdateInvoiceRequest, Invoice>().ReverseMap();
        CreateMap<CommonPaymentRequest, Payment>().ReverseMap();
        CreateMap<UpdatePaymentRequest, Payment>().ReverseMap();
        CreateMap<CommonPOPayScheduleRequest, POPaySchedule>().ReverseMap();
        CreateMap<UpdatePOPayScheduleRequest, POPaySchedule>().ReverseMap();
        CreateMap<CommonProjectActivityCostRequest, ProjectActivityCost>().ReverseMap();
        CreateMap<UpdateProjectActivityCost, ProjectActivityCost>().ReverseMap();
        CreateMap<CommonProjectActivityRequest, ProjectActivity>().ReverseMap();
        CreateMap<CommonProjectCategoryRequest, ProjectCategory>().ReverseMap();
        CreateMap<UpdateProjectCategory, ProjectCategory>().ReverseMap();
        CreateMap<CommonProjectRequest, Project>().ReverseMap();
        CreateMap<UpdateProjectRequest, Project>().ReverseMap();
        CreateMap<CommonProjectScheduleRequest, ProjectSchedule>().ReverseMap();
        CreateMap<UpdateProjectScheduleRequest, ProjectSchedule>().ReverseMap();
        CreateMap<CommonProjectTypeRequest, ProjectType>().ReverseMap();
        CreateMap<UpdateProjectTypeRequest, ProjectType>().ReverseMap();
        CreateMap<CommonPurchaseOrderRequest, PurchaseOrder>().ReverseMap();
        CreateMap<UpdatePurchaseOrderRequest, PurchaseOrder>().ReverseMap();
        CreateMap<CommonStaffRequest, Staff>().ReverseMap();
        CreateMap<UpdateStaffRequest, Staff>().ReverseMap();
        CreateMap<CommonSupplierRequest, Supplier>().ReverseMap();
        CreateMap<UpdateSupplierRequest, Supplier>().ReverseMap();

    }
    
}