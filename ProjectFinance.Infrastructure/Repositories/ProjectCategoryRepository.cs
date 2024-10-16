using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectCategoryRepository: GenericRepository<ProjectCategory>, IProjectCategoryRepository
{
    public ProjectCategoryRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<ProjectCategory>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectCategoryRepository));
            throw;
        }
    }
    
    public override async Task<ProjectCategory?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectCategoryRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(ProjectCategory projectCategoryEntity)
    {
        try
        {
            var projectCategory = await _dbSet.FirstOrDefaultAsync(x => x.Id == projectCategoryEntity.Id);
            if (projectCategory == null)
                return await Task.FromResult(false);

            projectCategory.Id = projectCategoryEntity.Id;
            projectCategory.Name = projectCategoryEntity.Name;
            // projectCategory.Description = projectCategoryEntity.Description;
            // projectCategory.IsActive = projectCategoryEntity.IsActive;

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectCategoryRepository));
            throw;
        }
    }
    
}