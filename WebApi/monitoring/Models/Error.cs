namespace WebApi.monitoring.Models
{
    public class Error
    {
        public static int Id()
        {
            return new Random().Next(100, 99999);
        }

        public static string LogMessage(string methodName, int errorId, Exception exception)
        {
            return $"{methodName} failed. ErrorId: {errorId}. Message: {exception.Message}. Trace: {exception.StackTrace}";
        }

        internal static string Display(int errorId)
        {
            return $"Ошибка операции. Код {errorId}";
        }
    }
}
