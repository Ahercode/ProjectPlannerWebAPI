using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Client>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ClientRepository));
            throw;
        }
    }
    
    public override async Task<Client?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ClientRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Client clientEntity)
    {
        try
        {
            
            var client = await _dbSet.FirstOrDefaultAsync(x=>x.Id == clientEntity.Id);
            if(client == null)
                return await Task.FromResult(false);
            
            client.Id = clientEntity.Id;
            client.Name = clientEntity.Name;
            client.Code = clientEntity.Code;
            client.Email = clientEntity.Email;
            client.Address = clientEntity.Address;
            client.Phone = clientEntity.Phone;
            
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ClientRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var client = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            if(client == null)
                return await Task.FromResult(false);
            
            _dbSet.Remove(client);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(ClientRepository));
            throw;
        }
    }
}