using Confluent.Kafka;
using Production_Company.Services;
using Production_Company.Entities;  
using Production_Company.DbContexts;
namespace Production_Company.BackgroundServices
{
    public class ProductionSchedulerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly KafkaProducerService _kafkaProducer;
        private readonly ILogger<KafkaProducerService> _logger;

        public ProductionSchedulerService(
            IServiceProvider serviceProvider,
            KafkaProducerService kafkaProducer
            , ILogger<KafkaProducerService> logger)
        {
            _serviceProvider = serviceProvider;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                  
                    var production = new Production
                    {
                        Count = Random.Shared.Next(1, 100),
                        Title = $"Production-{DateTime.Now.Ticks}",
                        ProductionTypeId = 1, // Assuming type exists
                        CreateDate = DateTimeOffset.Now,
                        Comment = "Scheduled Production"
                    };

                    dbContext.Productions.Add(production);
                    await dbContext.SaveChangesAsync();

                    await _kafkaProducer.ProduceEventAsync(production);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}