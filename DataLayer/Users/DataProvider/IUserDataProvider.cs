using DataLayer.Users.Models;

namespace DataLayer.Users.DataProvider
{
    public interface IUserDataProvider
    {
        Task SaveNew(User user);
    }
}
