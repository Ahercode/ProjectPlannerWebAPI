using AutoMapper;
using ProjectFinance.Domain.Dtos.Responses;
using ProjectFinance.Domain.Dtos.Responses.activity;
using ProjectFinance.Domain.Dtos.Responses.client;
using ProjectFinance.Domain.Dtos.Responses.costdetail;
using ProjectFinance.Domain.Dtos.Responses.financeoption;
using ProjectFinance.Domain.Dtos.Responses.invoice;
using ProjectFinance.Domain.Dtos.Responses.payment;
using ProjectFinance.Domain.Dtos.Responses.popayschedule;
using ProjectFinance.Domain.Dtos.Responses.project;
using ProjectFinance.Domain.Dtos.Responses.projectactivity;
using ProjectFinance.Domain.Dtos.Responses.projectactivitycost;
using ProjectFinance.Domain.Dtos.Responses.projectschedule;
using ProjectFinance.Domain.Dtos.Responses.projecttype;
using ProjectFinance.Domain.Dtos.Responses.purchaseorder;
using ProjectFinance.Domain.Dtos.Responses.staff;
using ProjectFinance.Domain.Dtos.Responses.supplier;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.Repositories;

namespace ProjectFinance.API.MappingProfiles;

public class DomainToResponse : Profile
    
{
    public DomainToResponse()
    {
        
        CreateMap<Activity, ActivityResponse>().ReverseMap();
        CreateMap<Bank, CommonResponse>().ReverseMap();
        CreateMap<Client, ClientResponse>().ReverseMap();
        CreateMap<CostCategory, CommonResponse>().ReverseMap();
        CreateMap<CostDetail, CostDetailResponse>().ReverseMap();
        CreateMap<Currency, CommonResponse>();
        CreateMap<FinanceOption, FinanceOptionResponse>().ReverseMap();
        CreateMap<FinanceOptionSchedule, CommonResponse>();
        CreateMap<Invoice, InvoiceResponse>().ReverseMap();
        CreateMap<Payment, PaymentResponse>().ReverseMap();
        CreateMap<POPaySchedule, POPayScheduleResponse>().ReverseMap();
        CreateMap<Project, ProjectResponse>().ReverseMap();
        CreateMap<ProjectActivity, ProjectActivityResponse>().ReverseMap();
        CreateMap<ProjectActivityCost, ProjectActivityCostResponse>().ReverseMap();
        CreateMap<ProjectSchedule, ProjectScheduleResponse>().ReverseMap();
        CreateMap<ProjectType, ProjectTypeResponse>().ReverseMap();
        CreateMap<PurchaseOrder, PurchaseOrderResponse>().ReverseMap();
        CreateMap<Staff, StaffResponse>().ReverseMap();
        CreateMap<Supplier, SupplierResponse>().ReverseMap();


        
    }
}