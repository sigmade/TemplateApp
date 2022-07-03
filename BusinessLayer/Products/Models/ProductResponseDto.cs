namespace BusinessLayer.Products.Models
{
    /// <summary>
    /// Модель отображения продуктов в списке
    /// </summary>
    public sealed class ProductResponseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
    }
}