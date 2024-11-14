using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Currency>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(CurrencyRepository));
            throw;
        }
    }
    
   public override async Task<Currency?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(CurrencyRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Currency currencyEntity)
    {
        try
        {

            var currency = await _dbSet.FirstOrDefaultAsync(x => x.Id == currencyEntity.Id);
            if (currency == null)
                return await Task.FromResult(false);

            currency.Id = currencyEntity.Id;
            currency.Name = currencyEntity.Name;
            currency.Code = currencyEntity.Code;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(CurrencyRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var currency = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (currency == null)
                return await Task.FromResult(false);

            _dbSet.Remove(currency);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(CurrencyRepository));
            throw;
        }
    }

}