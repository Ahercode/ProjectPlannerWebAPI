//
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using ProjectFinance.Domain.Entities;
// using ProjectFinance.Infrastructure.DBContext;
// using ProjectFinance.Infrastructure.Repositories.Interfaces;
//
// namespace ProjectFinance.Infrastructure.Repositories;
//
// public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
// {
//     public ActivityRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
//     {
//     }
//     
//     public override async Task<IEnumerable<Activity>> GetAll()
//     {
//         try
//         {
//             return await _dbSet
//                 .AsNoTracking()
//                 .AsSplitQuery()
//                 .ToListAsync();
//         }
//         catch (Exception e)
//         {
//             _Logger.LogError(e, "{Repo} AllActivities method error", typeof(ActivityRepository));
//             throw;
//         }
//     }
//
//     // public override async Task<> GetById(int id)
//     // {
//     //     try
//     //     {
//     //         return await _dbSet
//     //             .AsNoTracking()
//     //             .AsSplitQuery()
//     //             .FirstOrDefaultAsync(a => a.Id == id);
//     //     }
//     //     catch (Exception e)
//     //     {
//     //         _Logger.LogError(e, "{Repo} GetById method error", typeof(ActivityRepository));
//     //         throw;
//     //     }
//     // }
// }