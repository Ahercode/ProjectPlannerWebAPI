using ProjectFinance.Domain.Entities;

namespace ProjectFinance.Infrastructure.Repositories.Interfaces;

public interface IBankRepository : IGenericRepository<Bank>
{
    public  Task<Bank?> GetByCode(string code);
}