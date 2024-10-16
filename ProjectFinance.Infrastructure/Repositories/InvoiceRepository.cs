using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class InvoiceRepository: GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Invoice>> GetAll()
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} AllInvoices method error", typeof(InvoiceRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Invoice invoiceEntity)
    {
        try
        {

            var invoice = await _dbSet.FirstOrDefaultAsync(x => x.Id == invoiceEntity.Id);
            if (invoice == null)
                return await Task.FromResult(false);

            invoice.Id = invoice.Id;
            invoice.Amount = invoice.Amount;
            
            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(InvoiceRepository));
            throw;
        }

    }
}