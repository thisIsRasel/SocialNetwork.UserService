using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.FolloweeAggregate;

namespace UserService.Infrastructure.Repositories;
internal class FolloweeRepository : IFolloweeRepository
{
    private readonly UserDbContext _context;

    public FolloweeRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Followee followee)
    {
        _context.Followees.Add(followee);
    }

    public async Task<Followee?> GetAsync(Guid userId, Guid followeeUserId)
    {
        var followee = await _context.Followees
            .FirstOrDefaultAsync(f
                => f.UserId == userId && f.FolloweeUserId == followeeUserId);

        return followee;
    }

    public void Update(Followee followee)
    {
        _context.Followees.Update(followee);
    }
}
