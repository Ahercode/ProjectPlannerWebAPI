using ProjectFinance.Domain.Entities;

namespace ProjectFinance.Infrastructure.Repositories.Interfaces;

public interface IFinanceOptionRepository : IGenericRepository<FinanceOption>
{
    Task<FinanceOption?> GetByBankId(int? bankId);
}