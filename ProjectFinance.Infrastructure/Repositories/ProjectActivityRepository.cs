using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectActivityRepository: GenericRepository<ProjectActivity>, IProjectActivityRepository
{
    public ProjectActivityRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<ProjectActivity>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectActivityRepository));
            throw;
        }
    }
    
    // get by id
    public override async Task<ProjectActivity?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectActivityRepository));
            throw;
        }
    }
    
    // update
    public override async Task<bool> Update(ProjectActivity projectActivityEntity)
    {
        try
        {
            var projectActivity = await _dbSet.FirstOrDefaultAsync(x => x.Id == projectActivityEntity.Id);
            if (projectActivity == null)
                return await Task.FromResult(false);

            projectActivity.Id = projectActivityEntity.Id;
            projectActivity.ActivityId = projectActivityEntity.ActivityId;
            projectActivity.ProjectId = projectActivityEntity.ProjectId;
            projectActivity.Reference = projectActivityEntity.Reference;
            projectActivity.ContractorId = projectActivityEntity.ContractorId;
            projectActivity.Amount = projectActivityEntity.Amount;
            projectActivity.StartDate = projectActivityEntity.StartDate;
            projectActivity.EndDate = projectActivityEntity.EndDate;
            projectActivity.Note = projectActivityEntity.Note;
            projectActivity.FileName = projectActivityEntity.FileName;
            
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectActivityRepository));
            throw;
        }
    }
    
    // add
    public override async Task<bool> Add(ProjectActivity projectActivityEntity)
    {
        try
        {
            await _dbSet.AddAsync(projectActivityEntity);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Add method error", typeof(ProjectActivityRepository));
            throw;
        }
    }
    
    // delete
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var projectActivity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (projectActivity == null)
                return await Task.FromResult(false);

            _dbSet.Remove(projectActivity);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(ProjectActivityRepository));
            throw;
        }
    }
    
}