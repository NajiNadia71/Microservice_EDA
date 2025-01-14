using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Advertisement_Company.DbContexts;
using System;
using Advertisement_Company.Entities;
using System.Text.Json;
namespace Advertisement_Company
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private const string TopicName = "production-events";
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<KafkaConsumerService> logger)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "advertisement-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(TopicName);
            _logger.LogInformation("Kafka Consumer Service Started");
            _logger.LogInformation("Subscribed to Topic: {TopicName}", TopicName);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();

                    var productionEvent = JsonSerializer.Deserialize<Production>(consumeResult.Message.Value);

                    var production = new Production
                    {
                        Count = productionEvent.Count,
                        Title = productionEvent.Title,
                        ProductionTypeId = productionEvent.ProductionTypeId,
                        CreateDate = DateTimeOffset.Now,
                        Comment = "From Kafka Event"
                    };

                    dbContext.Productions.Add(production);
                    await dbContext.SaveChangesAsync();

                    var ad = new Ad
                    {
                        Title = "Come from Kafka Event Successfully",
                        ProductionId = production.Id,
                        CreateDate = DateTimeOffset.Now,
                        Text = $"Advertisement for production: {production.Title}"
                    };

                    dbContext.Ads.Add(ad);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}