using DataAccess.Repositories;

namespace DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    IRefreshTokenRepository RefreshTokens { get; }

    Task<int> SaveChangesAsync();
}