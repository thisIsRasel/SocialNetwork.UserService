using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.Aggregates.FriendRequestAggregate;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<UserDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("Default"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
        services.AddScoped<IFolloweeRepository, FolloweeRepository>();

        return services;
    }
}
