using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.FriendAggregate;

namespace UserService.Infrastructure.Repositories;
internal class FriendRepository : IFriendRepository
{
    private readonly UserDbContext _context;

    public FriendRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Friend?> GetAsync(Guid userId, Guid friendUserId)
    {
        var friend = await _context.Friends
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FriendUserId == friendUserId);

        return friend;
    }

    public void Add(Friend friend)
    {
        _context.Friends.Add(friend);
    }

    public void Update(Friend friend)
    {
        _context.Friends.Update(friend);
    }
}
