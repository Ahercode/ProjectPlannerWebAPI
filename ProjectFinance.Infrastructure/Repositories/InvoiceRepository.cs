using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

            invoice.Id = invoiceEntity.Id;
            invoice.Amount = invoiceEntity.Amount;
            invoice.InvoiceNumber = invoiceEntity.InvoiceNumber;
            invoice.SupplierId = invoiceEntity.SupplierId;
            invoice.DueDate = invoiceEntity.DueDate;
            invoice.ProjectId = invoiceEntity.ProjectId;
            invoice.PurchaseOrderId = invoiceEntity.PurchaseOrderId;
            
            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(InvoiceRepository));
            throw;
        }

    }
    
    // delete
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var invoice = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null)
                return await Task.FromResult(false);

            _dbSet.Remove(invoice);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(InvoiceRepository));
            throw;
        }
    }
}