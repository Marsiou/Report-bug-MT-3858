using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.Infrastructure.ServiceBus;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
     
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICorrelationContext _correlationContext;
        private readonly IServiceBus _serviceBus;

        public WeatherForecastController(
            ICorrelationContext correlationContext, 
            IServiceBus serviceBus, 
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _correlationContext = correlationContext;
            _serviceBus = serviceBus;
        }

        [HttpPut(Name = "publish message")]
        public async Task TryToPublishMessageWithCorrealtionId(string content, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Enter in route with correlationId{_correlationContext.CorrelationId}");
            await _serviceBus.PublishMessage(new MessageSample(content), cancellationToken);
        }
    }
}