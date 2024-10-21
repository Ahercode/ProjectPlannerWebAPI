namespace ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IActivityRepository Activities { get; }
    IFinanceOptionRepository FinanceOptions { get; }
    IBankRepository Banks { get; }
    IClientRepository Clients { get; }
    ICostCategoryRepository CostCategories { get; }
    ICostDetailRepository CostDetails { get; }
    ICurrencyRepository Currencies { get; }
    IFinanceOptionScheduleRepository FinanceOptionSchedules { get; }
    IInvoiceRepository Invoices { get; }
    IMonitoringEvaluationRepository MonitoringEvaluations { get; }
    IPaymentRepository Payments { get; }
    IPOPayScheduleRepository POPaySchedules { get; }
    IProjectRepository Projects { get; }
    IProjectCategoryRepository ProjectCategories { get; }
    IProjectActivityRepository ProjectActivities { get; }
    IProjectActivityCostRepository ProjectActivityCosts { get; }
    IProjectScheduleRepository ProjectSchedules { get; }
    IProjectTypeRepository ProjectTypes { get; }
    IPurchaseOrderRepository PurchaseOrders { get; }
    ISatffRepository Staffs { get; }
    ISupplierRepository Suppliers { get; }
    
    
    Task<bool> CompleteAsync();
}