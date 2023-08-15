using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.Aggregates.FriendAggregate;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.SeedWork;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFriendRepository, FriendRepository>();
        services.AddScoped<IFolloweeRepository, FolloweeRepository>();

        return services;
    }
}
