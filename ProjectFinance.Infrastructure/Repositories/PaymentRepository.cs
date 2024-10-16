using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Payment>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(PaymentRepository));
            throw;
        }
    }

    public override async Task<Payment?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(PaymentRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Payment paymentEntity)
    {
        try
        {
            
            var payment = await _dbSet.FirstOrDefaultAsync(x=>x.Id == paymentEntity.Id);
            if(payment == null)
                return await Task.FromResult(false);
            
            payment.Id = paymentEntity.Id;
            payment.Amount = paymentEntity.Amount;
            // payment.CurrencyId = paymentEntity.CurrencyId;
            // payment.PaymentDate = paymentEntity.PaymentDate;
            // payment.ProjectId = paymentEntity.ProjectId;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(PaymentRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var payment = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null)
                return await Task.FromResult(false);

            _dbSet.Remove(payment);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(PaymentRepository));
            throw;
        }
    }
}