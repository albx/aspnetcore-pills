using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace AspNetCorePills.Web;

public record MyLoggerConfiguration
{
    public string FilePath { get; set; } = string.Empty;
}

public class MyLogger : ILogger
{
    private readonly string _name;
    private readonly MyLoggerConfiguration _configuration;

    public MyLogger(string name, MyLoggerConfiguration configuration)
    {
        _name = name;
        _configuration = configuration;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        => default;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string[] logLines = [
            $"[{eventId.Id,2}: {logLevel,-12}]",
            $"[{_name}]: {formatter(state, exception)}",
        ];

        var filePath = "log.txt";
        if (!string.IsNullOrEmpty(_configuration.FilePath))
        {
            if (!Directory.Exists(_configuration.FilePath))
            {
                Directory.CreateDirectory(_configuration.FilePath);
            }

            filePath = Path.Combine(_configuration.FilePath, "log.txt");
        }

        File.AppendAllLines(
            filePath,
            logLines);
    }
}

[ProviderAlias("MyLogger")]
public class MyLoggerProvider : ILoggerProvider
{
    private readonly IOptionsMonitor<MyLoggerConfiguration> _configuration;
    public MyLoggerProvider(IOptionsMonitor<MyLoggerConfiguration> configurationOptions)
    {
        _configuration = configurationOptions;
    }
    public ILogger CreateLogger(string categoryName)
        => new MyLogger(categoryName, _configuration.CurrentValue);
    
    public void Dispose()
    {
        // Dispose of any resources if needed
    }
}

public static class MyLoggerProviderExtensions
{
    public static ILoggingBuilder AddMyLogger(
        this ILoggingBuilder builder, 
        Action<MyLoggerConfiguration> configure)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, MyLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <MyLoggerConfiguration, MyLoggerProvider>(builder.Services);

        builder.Services.Configure(configure);

        return builder;
    }
}
