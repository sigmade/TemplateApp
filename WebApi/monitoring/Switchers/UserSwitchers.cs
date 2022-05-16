namespace WebApi.monitoring.Switchers
{
    /// <summary>
    ///     Свитчеры позволяют отключать часть функционала, н-р при недоступности определенного сервиса
    /// </summary>
    public class UserSwitchers
    {
        /// <summary>
        ///     Доступен ли сервис добавления пользователя
        /// </summary>
        public bool AddServiceEnabed { get; set; }

        /// <summary>
        ///     Доступен ли сервис получения пользователей
        /// </summary>
        public bool GetServiceEnabled { get; set; }
    }
}
