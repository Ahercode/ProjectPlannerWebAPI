using ProjectFinance.Domain.Entities;

namespace ProjectFinance.Infrastructure.Repositories.Interfaces;

public interface IFinanceOptionScheduleRepository: IGenericRepository<FinanceOptionSchedule>
{
    Task<FinanceOptionSchedule> GetByFinanceOptionId(int? financeOptionId);
}