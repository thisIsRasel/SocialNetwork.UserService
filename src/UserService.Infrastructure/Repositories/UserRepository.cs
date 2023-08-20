using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Infrastructure.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User?> GetAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
}
