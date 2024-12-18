using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class FinanceOptionRepository : GenericRepository<FinanceOption>, IFinanceOptionRepository
{
    public FinanceOptionRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<FinanceOption>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(FinanceOptionRepository));
            throw;
        }
    }
    
    public override async Task<FinanceOption?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(FinanceOptionRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(FinanceOption financeOptionEntity)
    {
        try
        {

            var financeOption = await _dbSet.FirstOrDefaultAsync(x => x.Id == financeOptionEntity.Id);
            if (financeOption == null)
                return await Task.FromResult(false);

            financeOption.Id = financeOptionEntity.Id;
            financeOption.Description = financeOptionEntity.Description;
            financeOption.OptionType = financeOptionEntity.OptionType;
            financeOption.BankId = financeOptionEntity.BankId;

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(FinanceOptionRepository));
            throw;
        }
    }
    
    public async Task<FinanceOption?> GetByBankId(int? bankId)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.BankId == bankId);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetByBankId method error", typeof(FinanceOptionRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var financeOption = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (financeOption == null)
                return await Task.FromResult(false);

            _dbSet.Remove(financeOption);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(FinanceOptionRepository));
            throw;
        }
    }

}