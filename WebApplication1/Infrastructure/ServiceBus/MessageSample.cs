using MassTransit.KafkaIntegration;
using MassTransit.SagaStateMachine;

namespace WebApplication1.Infrastructure.ServiceBus
{
    public class MessageSample
    {
        public MessageSample(string body)
        {
            Body = body;
        }
        public string Body { get; }
    }
}