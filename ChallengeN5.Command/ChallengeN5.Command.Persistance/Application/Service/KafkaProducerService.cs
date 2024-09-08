namespace ChallengeN5.Command.Infrastructure.Application.Service;

using ChallengeN5.Command.Domain.Application.Service;
using Confluent.Kafka;
using System.Text.Json;
using System.Threading.Tasks;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly string _bootstrapServers;

    public KafkaProducerService(string bootstrapServers)
    {
        _bootstrapServers = bootstrapServers;
    }

    public async Task ProduceAsync(string topic, object data, CancellationToken cancellationToken)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

        using var producer = new ProducerBuilder<string, string>(config).Build();
        try
        {
            await producer.ProduceAsync(topic, new Message<string, string> { Key = $"{Guid.NewGuid()}", Value = JsonSerializer.Serialize(data) }, cancellationToken);
        }
        catch (Exception ex)
        {
            {
                throw new Exception($"Error producing message to topic {topic}: {ex.Message}");
            }
        }
    }
}