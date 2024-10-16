using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class StaffRepository : GenericRepository<Staff>, ISatffRepository
{
    public StaffRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Staff>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(StaffRepository));
            throw;
        }
    }

    public override async Task<Staff?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(StaffRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Staff staffEntity)
    {
        try
        {
            var staff = await _dbSet.FirstOrDefaultAsync(x => x.Id == staffEntity.Id);
            if (staff == null)
                return await Task.FromResult(false);

            staff.Id = staffEntity.Id;
            // staff.Name = staffEntity.Name;
            staff.Surname = staffEntity.Surname;
            staff.Email = staffEntity.Email;
            staff.Phone = staffEntity.Phone;
            // staff.BirthDate = staffEntity.BirthDate;

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(StaffRepository));
            throw;
        }
    }

    public async Task Add(Staff staff)
    {
        try
        {
            await _dbSet.AddAsync(staff);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Add method error", typeof(StaffRepository));
            throw;
        }
    }
}