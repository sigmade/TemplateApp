namespace WebApi.monitoring.Errors
{
    public interface IErrorHandler
    {
        string DefaultHandle(string methodName, Exception exception);
        string InputData(string methodName, Exception exception);
        string DisabledService(string methodName);
    }
}