using Confluent.Kafka;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApplication1.Infrastructure;
using WebApplication1.Infrastructure.ServiceBus;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddTransient<IServiceBus, KafkaProvider>();

builder.Services.TryAddScoped<ICorrelationContext, CorrelationContext>();
builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });

    x.AddRider(rider =>
    {
        rider.AddProducer<MessageSample>("topic");

        rider.UsingKafka((context, k) =>
        {
            k.Host("fill me", host =>
            {
                host.UseSasl(e => e.Mechanism = SaslMechanism.Plain);
            });
            k.SecurityProtocol = SecurityProtocol.Ssl;

            k.UseSendFilter(typeof(ContextSendFilter<>), context);
        });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
