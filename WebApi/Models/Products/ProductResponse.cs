namespace WebApi.Models.Products
{
    /// <summary>
    ///     Модель отображения продуктов в списке
    /// </summary>
    public class ProductResponse
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Дата добавления
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}