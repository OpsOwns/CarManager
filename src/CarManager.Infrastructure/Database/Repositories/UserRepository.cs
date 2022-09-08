using CarManager.Domain.Types;

namespace CarManager.Infrastructure.Database.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(CarManagerContext carManagerContext)
    {
        _users = carManagerContext.Users;
    }

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.Email == email,
            cancellationToken);

        return user ?? User.NotFound();
    }

    public async Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.Id == userId,
            cancellationToken);

        return user ?? User.NotFound();
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User existingUser)
    {
        _users.Update(existingUser);
        await Task.CompletedTask;
    }
}