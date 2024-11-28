using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectScheduleRepository : GenericRepository<ProjectSchedule>, IProjectScheduleRepository
{
    public ProjectScheduleRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<ProjectSchedule>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectScheduleRepository));
            throw;
        }
    }
    
    public override async Task<ProjectSchedule?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectScheduleRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(ProjectSchedule projectScheduleEntity)
    {
        try
        {
            var projectSchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == projectScheduleEntity.Id);
            if (projectSchedule == null)
                return await Task.FromResult(false);

            projectSchedule.Id = projectScheduleEntity.Id;
            projectSchedule.ProjectId = projectScheduleEntity.ProjectId;
            projectSchedule.Date = projectScheduleEntity.Date;
            projectSchedule.Amount = projectScheduleEntity.Amount;
            projectSchedule.InvoiceNumber = projectScheduleEntity.InvoiceNumber;
            projectSchedule.Reference = projectScheduleEntity.Reference;
            projectSchedule.Description = projectScheduleEntity.Description;

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectScheduleRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var projectSchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (projectSchedule == null)
                return await Task.FromResult(false);

            _dbSet.Remove(projectSchedule);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(ProjectScheduleRepository));
            throw;
        }
    }
}