namespace WebApi.Models.Users
{
    /// <summary>
    /// Модель добавления нового пользователя
    /// </summary>
    public class AddUserRequest
    {
        /// <summary>
        /// Логин пользователя (номер телефона)
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
