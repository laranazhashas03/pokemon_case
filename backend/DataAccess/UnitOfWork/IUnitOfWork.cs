using DataAccess.Repositories;

namespace DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync();
}