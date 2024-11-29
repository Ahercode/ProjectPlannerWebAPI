using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
{
 public PurchaseOrderRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
 {
 }
 
 public override async Task<IEnumerable<PurchaseOrder>> GetAll()
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
         _Logger.LogError(e, "{Repo} GetAll method error", typeof(PurchaseOrderRepository));
         throw;
     }
 }
 
 public override async Task<PurchaseOrder?> GetById(int id)
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
         _Logger.LogError(e, "{Repo} GetById method error", typeof(PurchaseOrderRepository));
         throw;
     }
 }

 public override async Task<bool> Update(PurchaseOrder purchaseOrderEntity)
 {
     try
     {

         var purchaseOrder = await _dbSet.FirstOrDefaultAsync(x => x.Id == purchaseOrderEntity.Id);
         if (purchaseOrder == null)
             return await Task.FromResult(false);

         purchaseOrder.Id = purchaseOrderEntity.Id;
         purchaseOrder.PONumber = purchaseOrderEntity.PONumber;
         purchaseOrder.Date = purchaseOrderEntity.Date;
         purchaseOrder.SupplierId = purchaseOrderEntity.SupplierId;
         purchaseOrder.ActivityId = purchaseOrderEntity.ActivityId;
         purchaseOrder.ProjectId = purchaseOrderEntity.ProjectId;
         purchaseOrder.Reference = purchaseOrderEntity.Reference;
         purchaseOrder.FileName = purchaseOrderEntity.FileName;
         
         return true;
     }
     catch (Exception e)
     {
         _Logger.LogError(e, "{Repo} Update method error", typeof(PurchaseOrderRepository));
         throw;
     }
 }

 public async Task<PurchaseOrder?> GetByProjectId(int? projectId)
     {
         try
         {
             return await _dbSet
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(b => b.ProjectId == projectId);
         }
         catch (Exception e)
         {
             _Logger.LogError(e, "{Repo} GetByProjectId method error", typeof(PurchaseOrderRepository));
             throw;
         }
     }
}