

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectTypeRepository : GenericRepository<ProjectType>, IProjectTypeRepository
{
    public ProjectTypeRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<ProjectType>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectTypeRepository));
            throw;
        }
    }
    
    public override async Task<ProjectType?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectTypeRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(ProjectType projectTypeEntity)
    {
        try
        {
            
            var projectType = await _dbSet.FirstOrDefaultAsync(x=>x.Id == projectTypeEntity.Id);
            if(projectType == null)
                return await Task.FromResult(false);
            
            projectType.Id = projectTypeEntity.Id;
            projectType.Name = projectTypeEntity.Name;
            // projectType.Description = projectTypeEntity.Description;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectTypeRepository));
            throw;
        }
    }
    
    public async Task<ProjectType?> GetByName(string name)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.Name == name);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetByName method error", typeof(ProjectTypeRepository));
            throw;
        }
    }
}