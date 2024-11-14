using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class PODetailReceiveRepository : GenericRepository<PODetailReceive>, IPODetailReceiveRepository
{
    public PODetailReceiveRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<PODetailReceive>> GetAll()
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
            _Logger.LogError(e, "{Repo} All function error", typeof(PODetailReceiveRepository));
            throw;
        }
        
    }
    
    public override async Task<PODetailReceive?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById function error", typeof(PODetailReceiveRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(PODetailReceive poDetailReceiveEntity)
    {
        try
        {
            var poDetailReceive = await _dbSet.FirstOrDefaultAsync(x=>x.Id == poDetailReceiveEntity.Id);
            if(poDetailReceive == null)
                return false;
            
            poDetailReceive.Id = poDetailReceiveEntity.Id;
            poDetailReceive.PODetailId = poDetailReceiveEntity.PODetailId;
            poDetailReceive.QunatityReceived = poDetailReceiveEntity.QunatityReceived;
            poDetailReceive.ReceivedDate = poDetailReceiveEntity.ReceivedDate;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update function error", typeof(PODetailReceiveRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var poDetailReceive = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            if(poDetailReceive == null)
                return false;
            
            _dbSet.Remove(poDetailReceive);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete function error", typeof(PODetailReceiveRepository));
            throw;
        }
    }
}