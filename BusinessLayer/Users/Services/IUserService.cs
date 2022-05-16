using BusinessLayer.Users.Models;

namespace BusinessLayer.Users.Services
{
    public interface IUserService
    {
        Task AddNew(UserDto user);
    }
}
