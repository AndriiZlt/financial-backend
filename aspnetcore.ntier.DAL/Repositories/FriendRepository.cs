using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;

namespace aspnetcore.ntier.DAL.Repositories;

public class FriendRepository : GenericRepository<Friend>, IFriendRepository
{
    private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
    public FriendRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) : base(aspNetCoreNTierDbContext)
    {
        _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
    }

}
