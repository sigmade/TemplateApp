namespace WebApi.monitoring.Errors
{
    /// <summary>
    /// Базовый обработчик ошибок
    /// </summary>
    public sealed class ErrorHandler : IErrorHandler
    {
        private readonly Serilog.ILogger _logger;

        public ErrorHandler(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Генерация кода ошибка для отображения пользователю и запись в лог
        /// </summary>
        /// <returns>Код ошибки</returns>
        public static int Id()
        {
            return new Random().Next(100, 99999);
        }

        /// <summary>
        /// Стандартная обработка ошибки и запись в лог
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="exception"></param>
        /// <returns>
        /// Отображаемый текст ошибки
        /// </returns>
        public string DefaultHandle(string methodName, Exception exception)
        {
            var errorId = Id();
            _logger.Error($"{methodName} failed. ErrorId: {errorId}. Message: {exception.Message}. Trace: {exception.StackTrace}");
            return $"Ошибка операции. Код {errorId}";
        }

        /// <summary>
        /// Отображение ошибки при отключенном сервисе и запись в лог
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns>
        /// Отображаемый текст ошибки
        /// </returns>
        public string DisabledService(string methodName)
        {
            _logger.Information($"Call disabled service in: {methodName}");
            return $"Сервис временно не доступен, попробуйте позже";
        }

        /// <summary>
        /// Отображение ошибки при невалидных входных данных
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns>
        /// Отображаемый текст ошибки
        /// </returns>
        public string InputData(string methodName, Exception exception)
        {
            var errorId = Id();
            _logger.Error($"No valid data in {methodName}. ErrorId: {errorId}. Message: {exception.Message}. Trace: {exception.StackTrace}");
            return $"Некорректные входных данные. Код ошибки {errorId}";
        }
    }
}