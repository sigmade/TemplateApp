using DataLayer.EF;
using DataLayer.Users.Models;

namespace DataLayer.Users.DataProvider
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public UserDataProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveNew(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }        
    }
}
