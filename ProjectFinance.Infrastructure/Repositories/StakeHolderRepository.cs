using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class StakeHolderRepository: GenericRepository<StakeHolder>, IStakeHolderRepository
{
    public StakeHolderRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<StakeHolder>> GetAll()
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
            _Logger.LogError(e, "{Repo} All method error", typeof(StakeHolderRepository));
            throw;
        }
    }
    
    public override async Task<StakeHolder?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(StakeHolderRepository));
            throw;
        }
    }
    
    // update method
    public override async Task<bool> Update(StakeHolder stakeHolderDto)
    {
        try
        {
            var stakeHolder = await _dbSet.FirstOrDefaultAsync(x=>x.Id == stakeHolderDto.Id);
            
            if(stakeHolder == null)
                return await Task.FromResult(false);
            
            stakeHolder.Id = stakeHolderDto.Id;
            stakeHolder.Name = stakeHolderDto.Name;
            stakeHolder.Email = stakeHolderDto.Email;
            stakeHolder.Phone = stakeHolderDto.Phone;
            stakeHolder.Address = stakeHolderDto.Address;
            stakeHolder.Designation = stakeHolderDto.Designation;
            stakeHolder.ItemId = stakeHolderDto.ItemId;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(StakeHolderRepository));
            throw;
        }
    }
    
    // delete method
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var stakeHolder = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            
            if(stakeHolder == null)
                return await Task.FromResult(false);
            
            _dbSet.Remove(stakeHolder);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(StakeHolderRepository));
            throw;
        }
    }
}