namespace WebApi.monitoring.Switchers
{
    /// <summary>
    /// Свитчеры позволяют отключать часть функционала, н-р при недоступности определенного сервиса
    /// </summary>
    public class ProductSwitchers
    {
        /// <summary>
        /// Доступен ли сервис добавления продукта
        /// </summary>
        public bool AddServiceEnabed { get; set; }

        /// <summary>
        /// Доступен ли сервис получения продуктов
        /// </summary>
        public bool GetServiceEnabled { get; set; }
    }
}