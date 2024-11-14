using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{  
    public SupplierRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Supplier>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(SupplierRepository));
            throw;
        }
    }

    public override async Task<Supplier?> GetById(int id)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetById method error", typeof(SupplierRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Supplier supplierEntity)
    {
        try
        {
            var supplier = await _dbSet.FirstOrDefaultAsync(x => x.Id == supplierEntity.Id);
            if (supplier == null)
                return await Task.FromResult(false);

            supplier.Id = supplierEntity.Id;
            supplier.Name = supplierEntity.Name;
            supplier.Code = supplierEntity.Code;
            supplier.Address = supplierEntity.Address;
            supplier.Email = supplierEntity.Email;
            supplier.Phone = supplierEntity.Phone;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(SupplierRepository));
            throw;
        }
    }
    
    // delete
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var supplier = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null)
                return await Task.FromResult(false);

            _dbSet.Remove(supplier);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(SupplierRepository));
            throw;
        }
    }
}