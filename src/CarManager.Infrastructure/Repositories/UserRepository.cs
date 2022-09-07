namespace CarManager.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(User.NotFound());
    }

    public async Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(User.NotFound());
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User existingUser)
    {
        return Task.CompletedTask;
    }
}