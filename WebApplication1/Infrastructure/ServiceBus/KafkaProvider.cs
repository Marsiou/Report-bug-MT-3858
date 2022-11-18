using MassTransit;

namespace WebApplication1.Infrastructure.ServiceBus
{
    public class KafkaProvider : IServiceBus
    {
        private readonly ICorrelationContext _correlationContext;
        private readonly ITopicProducer<MessageSample> _kafkaProducer;
        private readonly ILogger<CorrelationContext> _logger;

        public KafkaProvider(ICorrelationContext correlationContext,
                             ITopicProducer<MessageSample> kafkaProducer,
                             ILogger<CorrelationContext> logger)
        {
            _correlationContext = correlationContext;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }
        public async Task PublishMessage(MessageSample message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering in {nameof(PublishMessage)} with correlationId{_correlationContext.CorrelationId}");
            await _kafkaProducer.Produce(message, cancellationToken);
        }

    }
}