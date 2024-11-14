using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly ILogger _Logger;
    private readonly ProjectFinanceContext _Context;
    internal DbSet<T> _dbSet;
    
    public GenericRepository(ProjectFinanceContext context, ILogger logger)
    {
        _Context = context;
        _Logger = logger;
        _dbSet = _Context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> GetByCode(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.GetType().GetProperty("Code").GetValue(x).ToString() == code);
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        return true;
    }

    public virtual async Task<bool> Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;
        
        _dbSet.Remove(entity);
        return true;
    }
}