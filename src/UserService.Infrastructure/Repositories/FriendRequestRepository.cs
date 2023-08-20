using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.FriendRequestAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Infrastructure.Repositories;
internal class FriendRequestRepository : IFriendRequestRepository
{
    private readonly UserDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public FriendRequestRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<FriendRequest?> GetAsync(
        Guid userId,
        Guid friendUserId)
    {
        var friend = await _context.FriendRequests
            .FirstOrDefaultAsync(x
                => x.UserId == userId && x.FriendUserId == friendUserId);

        return friend;
    }

    public void Add(FriendRequest friendRequest)
    {
        _context.FriendRequests.Add(friendRequest);
    }

    public void Update(FriendRequest friendRequest)
    {
        _context.Entry(friendRequest).State = EntityState.Modified;
    }
}
