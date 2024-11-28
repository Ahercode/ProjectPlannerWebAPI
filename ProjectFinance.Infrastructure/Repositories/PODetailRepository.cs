using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class PODetailRepository : GenericRepository<PODetail>, IPODetailRepository
{
    public PODetailRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<PODetail>> GetAll()
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
            _Logger.LogError(e, "{Repo} All function error", typeof(PODetailRepository));
            throw;
        }
        
    }

    public override async Task<PODetail?> GetById(int id)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetById function error", typeof(PODetailRepository));
            throw;
        }
    }

    public override async Task<bool> Update(PODetail poDetailEntity)
    {
        try
        {
            var poDetail = await _dbSet.FirstOrDefaultAsync(x=>x.Id == poDetailEntity.Id);
            if(poDetail == null)
                return false;
            
            poDetail.Id = poDetailEntity.Id;
            poDetail.PurchaseOrderId = poDetailEntity.PurchaseOrderId;
            poDetail.CostDetailId = poDetailEntity.CostDetailId;
            poDetail.Quantity = poDetailEntity.Quantity;
            poDetail.Amount = poDetailEntity.Amount;
            poDetail.Date = poDetailEntity.Date;  
                
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update function error", typeof(PODetailRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var poDetail = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            if(poDetail == null)
                return false;
            
            _dbSet.Remove(poDetail);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete function error", typeof(PODetailRepository));
            throw;
        }
    }
}