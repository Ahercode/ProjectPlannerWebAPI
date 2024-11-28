using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
   public ProjectRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
   {
   }
   
   public override async Task<IEnumerable<Project>> GetAll()
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
           _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectRepository));
           throw;
       }
   }
   
   public override async Task<Project?> GetById(int id)
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
           _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectRepository));
           throw;
       }
   }

   public override async Task<bool> Update(Project projectEntity)
   {
       try
       {

           var project = await _dbSet.FirstOrDefaultAsync(x => x.Id == projectEntity.Id);
           if (project == null)
               return await Task.FromResult(false);

           project.Id = projectEntity.Id;
           project.Name = projectEntity.Name;
           project.Code = projectEntity.Code;
           project.ProjectTypeId = projectEntity.ProjectTypeId;
           project.ProjectCategoryId = projectEntity.ProjectCategoryId;
           project.CurrencyId = projectEntity.CurrencyId;
           project.StartDate = projectEntity.StartDate;
           project.EndDate = projectEntity.EndDate;
           project.Status = projectEntity.Status;
           project.ClientId = projectEntity.ClientId;
           project.ContractSum = projectEntity.ContractSum;
           project.Note = projectEntity.Note;
           project.Location = projectEntity.Location;
           project.ContractorId = projectEntity.ContractorId;
           project.ProjectPriority = projectEntity.ProjectPriority;

           return true;
       }
       catch (Exception e)
       {
           _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectRepository));
           throw;
       }
   }

}