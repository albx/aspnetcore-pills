namespace AspNetCorePills.Web;

class MyService
{
    private static int value = 0;
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        value++;
        _logger = logger;
    }

    public int GetValue()
    {
        _logger.LogInformation(
            "Value: {Value}", 
            value);

        return value;
    }
}