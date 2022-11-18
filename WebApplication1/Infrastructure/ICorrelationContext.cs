namespace WebApplication1.Infrastructure
{
    public interface ICorrelationContext
    {
        public Guid CorrelationId { get; set; }
    }
}
