using Confluent.Kafka;
using Production_Company.Entities;
using System.Text.Json;
namespace Production_Company.Services
{
    public class KafkaProducerService
    {
        private readonly IProducer<string, string> _producer;
        private const string TopicName = "production-events";
      
        public KafkaProducerService(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
          
        }

        public async Task ProduceEventAsync(Production production)
        {
            var message = new Message<string, string>
            {
                Key = production.Id.ToString(),
                Value = JsonSerializer.Serialize(production)
            };

            await _producer.ProduceAsync(TopicName, message);
        }
    }
}
