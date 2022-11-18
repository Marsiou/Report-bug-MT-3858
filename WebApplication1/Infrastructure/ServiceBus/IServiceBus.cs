namespace WebApplication1.Infrastructure.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishMessage(MessageSample message, CancellationToken cancellationToken);
    }
}
