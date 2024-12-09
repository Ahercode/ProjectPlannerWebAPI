using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectDocumentRepository : GenericRepository<ProjectDocument>, IProjectDocumentRepository
{
    public ProjectDocumentRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<ProjectDocument>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectDocumentRepository));
            throw;
        }
    }
    
    public override async  Task<bool> Add(ProjectDocument entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Add method error", typeof(ProjectDocumentRepository));
            throw;

        }
    }
    
    // update
    public override async Task<bool> Update(ProjectDocument entity)
    {
        try
        {
            var projectDocument = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            
            if (projectDocument == null)
                return await Task.FromResult(false);
            
            projectDocument.ProjectId = entity.ProjectId;
            projectDocument.DocUrl = entity.DocUrl;
            projectDocument.Note = entity.Note;
            
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectDocumentRepository));
            throw;
        }
    }
    
    // delete
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var projectDocument = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            
            if (projectDocument == null)
                return await Task.FromResult(false);
            
            _dbSet.Remove(projectDocument);
            
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(ProjectDocumentRepository));
            throw;
        }
    }
}