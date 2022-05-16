using BusinessLayer.Users.Models;
using BusinessLayer.Users.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models.Users;
using WebApi.monitoring.Errors;
using WebApi.monitoring.Switchers;

namespace WebApi.Controllers
{
    /// <summary>
    ///     Контроллер управления пользователями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ErrorHandler _error;
        private readonly IUserService _userService;
        private readonly IOptionsMonitor<UserSwitchers> _userSwitchers;

        public UsersController(
            ErrorHandler error,
            IUserService userService,
            IOptionsMonitor<UserSwitchers> userSwitchers)
        {
            _error = error;
            _userService = userService;
            _userSwitchers = userSwitchers;
        }

        /// <summary>
        ///     Добавление нового пользователя
        /// </summary>
        /// <remarks>
        ///     Пример запроса:
        ///     {
        ///     "Login": "7073332211",
        ///     "Password": "Super|-|ardPass#ord"
        ///     }
        /// </remarks>
        /// <param name="request">Модель нового пользователя</param>
        /// <returns>Статус 204 NoContent</returns>
        /// <response code="500">Прочие ошибки сервера</response>
        /// <response code="503">Принудительно выключен сервис</response>
        /// <response code="204">Успешный ответ без возвращаемого значения</response>
        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddNewUser(AddUserRequest request)
        {
            if (!_userSwitchers.CurrentValue.AddServiceEnabed)
            {
                return StatusCode(503, _error.DisabledService(nameof(AddNewUser)));
            }

            try
            {
                await _userService.AddNew(new UserDto
                {
                    Login = request.Login,
                    Password = request.Password
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, _error.DefaultHandle(nameof(AddNewUser), ex));
            }

            return NoContent();
        }
    }
}
