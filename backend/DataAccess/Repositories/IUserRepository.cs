using Entity.Entities;

namespace DataAccess.Repositories;

// Defines database operations related to users.
public interface IUserRepository
{
    // Finds a user by their email address.
    Task<User?> GetByEmailAsync(string email);

    // Finds a user by their ID.
    Task<User?> GetByIdAsync(int id);

    // Adds a new user to the database context.
    Task AddAsync(User user);
}