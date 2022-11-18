namespace WebApplication1.Infrastructure
{
    public class CorrelationContext : ICorrelationContext
    {
        private readonly ILogger<CorrelationContext> _logger;
        public CorrelationContext(ILogger<CorrelationContext> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Create instance {nameof(CorrelationContext)}");
            CorrelationId = Guid.NewGuid();
        }
        public Guid CorrelationId { get ; set ; }
    }
}
