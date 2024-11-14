using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        CreateMap<CommonCreateRequest, Currency>().ReverseMap();
        CreateMap<CommonUpdateRequest, Currency>().ReverseMap();
        CreateMap<CommonCreateRequest, CostCategory>().ReverseMap();
        CreateMap<CommonUpdateRequest, CostCategory>().ReverseMap();
        CreateMap<ActivityCreateRequest, Activity>().ReverseMap();
        CreateMap<ActivityUpdateRequest, Activity>().ReverseMap();
        CreateMap<ClientCreateRequest, Client>().ReverseMap();
        CreateMap<ClientUpdateRequest, Client>().ReverseMap();
        CreateMap<CostDetailCreateRequest, CostDetail>().ReverseMap();
        CreateMap<CostDetailUpdateRequest, CostDetail>().ReverseMap();
        CreateMap<FinanceOptionCreateRequest, FinanceOption>().ReverseMap();
        CreateMap<UpdateFinanceOptionRequest, FinanceOption>().ReverseMap();
        CreateMap<FinanceOptionScheduleCreateRequest, FinanceOptionSchedule>().ReverseMap();
        CreateMap<UpdateFinanceOptionScheduleRequest, FinanceOptionSchedule>().ReverseMap();
        CreateMap<InvoiceCreateRequest, Invoice>().ReverseMap();
        CreateMap<UpdateInvoiceRequest, Invoice>().ReverseMap();
        CreateMap<MonitoringEvaluationCreateRequest, MonitoringEvaluation>().ReverseMap();
        CreateMap<MonitoringEvaluationUpdateRequest, MonitoringEvaluation>().ReverseMap();
        CreateMap<PaymentCreateRequest, Payment>().ReverseMap();
        CreateMap<UpdatePaymentRequest, Payment>().ReverseMap();
        CreateMap<POPayScheduleRequest, POPaySchedule>().ReverseMap();
        CreateMap<UpdatePOPayScheduleRequest, POPaySchedule>().ReverseMap();
        CreateMap<ProjectActivityCostRequest, ProjectActivityCost>().ReverseMap();
        CreateMap<UpdateProjectActivityCost, ProjectActivityCost>().ReverseMap();
        CreateMap<ProjectActivityRequest, ProjectActivity>().ReverseMap();
        CreateMap<UpdateProjectActivity, ProjectActivity>().ReverseMap();
        CreateMap<CommonCreateRequest, ProjectCategory>().ReverseMap();
        CreateMap<CommonUpdateRequest, ProjectCategory>().ReverseMap();
        CreateMap<ProjectRequest, Project>().ReverseMap();
        CreateMap<UpdateProjectRequest, Project>().ReverseMap();
        CreateMap<ProjectScheduleCreateRequest, ProjectSchedule>().ReverseMap();
        CreateMap<UpdateProjectScheduleRequest, ProjectSchedule>().ReverseMap();
        CreateMap<CommonCreateRequest, ProjectType>().ReverseMap();
        CreateMap<CommonUpdateRequest, ProjectType>().ReverseMap();
        CreateMap<PurchaseOrderRequest, PurchaseOrder>().ReverseMap();
        CreateMap<UpdatePurchaseOrderRequest, PurchaseOrder>().ReverseMap();
        CreateMap<StaffRequest, Staff>().ReverseMap();
        CreateMap<UpdateStaffRequest, Staff>().ReverseMap();
        CreateMap<SupplierCreateRequest, Supplier>().ReverseMap();
        CreateMap<UpdateSupplierRequest, Supplier>().ReverseMap();
        CreateMap<ContractorCreateRequest, Contractor>().ReverseMap();
        CreateMap<ContractorUpdateRequest, Contractor>().ReverseMap();
        CreateMap<StakeHolderCreateRequest, StakeHolder>().ReverseMap();
        CreateMap<StakeHolderUpdateRequest, StakeHolder>().ReverseMap();
        CreateMap<PODetailCreateRequest, PODetail>().ReverseMap();
        CreateMap<PODetailUpdateRequest, PODetail>().ReverseMap();
        CreateMap<PODetailReceiveCreateRequest, PODetailReceive>().ReverseMap();
        CreateMap<PODetailReceiveUpdateRequest, PODetailReceive>().ReverseMap();
    }
    
}