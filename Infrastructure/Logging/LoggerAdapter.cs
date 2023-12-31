using Application.Contracts.Logging;

namespace Infrastructure.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
	private readonly IAppLogger<T> _logger;

    public LoggerAdapter(IAppLogger<T> logger)
    {
        _logger = logger;
    }

	public void LogInformation(string message, params object[] args)
	{
		_logger.LogInformation(message, args);
	}

	public void LogWarning(string message, params object[] args)
	{
		_logger.LogWarning(message, args);
	}
}
