using Microsoft.Extensions.Logging;
using ProjectFinance.Infrastructure.DBContext;

namespace ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ProjectFinanceContext _context;

    public UnitOfWork(ProjectFinanceContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("logs");
        // Activities = new ActivityRepository(context, loggerFactory.CreateLogger<ActivityRepository>());
        FinanceOptions = new FinanceOptionRepository(context, logger);
        Banks = new BankRepository(context, logger);
        Clients = new ClientRepository(context, logger);
        CostCategories = new CostCategoryRepository(context, logger);
        CostDetails = new CostDetailRepository(context, logger);
        Currencies = new CurrencyRepository(context, logger);
       
    }

    public IActivityRepository Activities { get; }
    public IFinanceOptionRepository FinanceOptions { get; }
    public IBankRepository Banks { get; }
    public IClientRepository Clients { get; }
    public ICostCategoryRepository CostCategories { get; }
    public ICostDetailRepository CostDetails { get; }
    public ICurrencyRepository Currencies { get; }
    public IFinanceOptionScheduleRepository FinanceOptionSchedules { get; }
    public IInvoiceRepository Invoices { get; }
    public IPaymentRepository Payments { get; }
    public IPOPayScheduleRepository POPaySchedules { get; }
    public IProjectRepository Projects { get; }
    public IProjectCategoryRepository ProjectCategories { get; }
    public IProjectActivityRepository ProjectActivities { get; }
    public IProjectActivityCostRepository ProjectActivityCosts { get; }
    public IProjectScheduleRepository ProjectSchedules { get; }
    public IProjectTypeRepository ProjectTypes { get; }
    public IPurchaseOrderRepository PurchaseOrders { get; }
    public ISatffRepository Staffs { get; }
    public ISupplierRepository Suppliers { get; }
    
    
    
    public async Task<bool> CompleteAsync()
    {
        var result =  await _context.SaveChangesAsync();
        
        return result >0;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}