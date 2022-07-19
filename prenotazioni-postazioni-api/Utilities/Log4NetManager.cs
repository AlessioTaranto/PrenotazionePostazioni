namespace prenotazioni_postazioni_api.Utilities
{
    public class Log4NetManager<T>
    {
        private Log4NetManager(){}

        public static ILogger<T> GetLogger()
        {
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole());

            return loggerFactory.CreateLogger<T>();
            
    
        }

    }
}
