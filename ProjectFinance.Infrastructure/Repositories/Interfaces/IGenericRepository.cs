namespace ProjectFinance.Infrastructure.Repositories.Interfaces;

public interface IGenericRepository <T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> GetByCode(string code);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
}