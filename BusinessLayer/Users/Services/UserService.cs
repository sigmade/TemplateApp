using BusinessLayer.Users.Models;
using DataLayer.Users.DataProvider;

namespace BusinessLayer.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataProvider _dataProvider;

        public UserService(IUserDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task AddNew(UserDto user)
        {
            await _dataProvider.SaveNew(new() { Login = user.Login, Password = user.Password });
        }
    }
}
