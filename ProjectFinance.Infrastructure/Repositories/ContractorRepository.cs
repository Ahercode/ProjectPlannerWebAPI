using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ContractorRepository : GenericRepository<Contractor>, IContractorRepository
{
    public ContractorRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Contractor>> GetAll()
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
            _Logger.LogError(e, "{Repo} All method error", typeof(ContractorRepository));
            throw;
        }
    }

    public override async Task<Contractor?> GetById(int id)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.id == id);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ContractorRepository));
            throw;
        }
    }
    
    // update method
    public override async Task<bool> Update(Contractor contractorDto)
    {
        try
        {
            var contractor = await _dbSet.FirstOrDefaultAsync(x=>x.id == contractorDto.id);
            
            if(contractor == null)
                return await Task.FromResult(false);
            
            contractor.id = contractorDto.id;
            contractor.address = contractorDto.address;
            contractor.email = contractorDto.email;
            contractor.name = contractorDto.name;
            contractor.phone = contractorDto.phone;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ContractorRepository));
            throw;
        }
    }
    
    // add method
    public override async Task<bool> Add(Contractor contractorDto)
    {
        try
        {
            await _dbSet.AddAsync(contractorDto);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Add method error", typeof(ContractorRepository));
            throw;
        }
    }
    
    // delete method
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var contractor = await _dbSet.FirstOrDefaultAsync(x=>x.id == id);
            
            if(contractor == null)
                return await Task.FromResult(false);
            
            _dbSet.Remove(contractor);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(ContractorRepository));
            throw;
        }
    }

    
}