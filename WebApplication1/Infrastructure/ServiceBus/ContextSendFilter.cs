using MassTransit;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Infrastructure.ServiceBus
{
    public class ContextSendFilter<T> :
          IFilter<SendContext<T>>
         where T : class
    {
        private readonly ICorrelationContext _correlationContext;
        private readonly ILogger<CorrelationContext> _logger;
        
        public ContextSendFilter(ICorrelationContext correlationContext, ILogger<CorrelationContext> logger)
        {
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public void Probe(ProbeContext context)
        {
        }

        public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            _logger.LogInformation($"Enter in middleware with correlationId{_correlationContext.CorrelationId}");
            context.Headers.Set("applicationId", "");
            context.Headers.Set("tenantId", "");
            context.Headers.Set("correlationId", _correlationContext);
            context.Headers.Set("lang", "");
            await next.Send(context);
        }
    }
}
