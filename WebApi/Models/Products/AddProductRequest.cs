namespace WebApi.Models.Products
{
    /// <summary>
    ///     Модель добавления нового продукта
    /// </summary>
    public class AddProductRequest
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Описание
        /// </summary>
        public string Description { get; set; }
    }
}