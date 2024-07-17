using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class BankRepository : GenericRepository<Bank> , IBankRepository
{
    public BankRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Bank>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(BankRepository));
            throw;
        }
    }

    public override async Task<Bank?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(BankRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Bank bankEntity)
    {
        try
        {
            
            var bank = await _dbSet.FirstOrDefaultAsync(x=>x.Id == bankEntity.Id);
            if(bank == null)
                return await Task.FromResult(false);
            
            bank.Id = bankEntity.Id;
            bank.Name = bankEntity.Name;
            bank.Code = bankEntity.Code;
            
            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(BankRepository));
            throw;
        }
    }
    
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
           var bank = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
           if(bank == null)
               return false;
           return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(BankRepository));
            throw;
        }
    }

    public override async Task<Bank?> GetByCode(string code)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.Code == code);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetByCode method error", typeof(BankRepository));
            throw;
        }
    }
}