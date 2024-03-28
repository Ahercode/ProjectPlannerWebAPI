using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class CostDetailRepository : GenericRepository<CostDetail>, ICostDetailRepository
{
    public CostDetailRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<CostDetail>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(CostDetailRepository));
            throw;
        }
    }
    
    public override async Task<CostDetail?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(CostDetailRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(CostDetail costDetailEntity)
    {
        try
        {
            
            var costDetail = await _dbSet.FirstOrDefaultAsync(x=>x.Id == costDetailEntity.Id);
            if(costDetail == null)
                return await Task.FromResult(false);
            
            costDetail.Id = costDetailEntity.Id;
            costDetail.Name = costDetailEntity.Name;
            costDetail.Code = costDetailEntity.Code;
            costDetail.CostCategoryId = costDetailEntity.CostCategoryId;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(CostDetailRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var costDetail = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            if(costDetail == null)
                return await Task.FromResult(false);
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(CostDetailRepository));
            throw;
        }
    }
}