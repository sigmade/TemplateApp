namespace BusinessLayer.Products.Models
{
    /// <summary>
    ///     Модель добавления нового продукта
    /// </summary>
    public class ProductDto
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