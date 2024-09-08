namespace ChallengeN5.Command.Infrastructure.Application.Service;

using ChallengeN5.Query.Domain.Application.Model;
using ChallengeN5.Query.Domain.Application.Service;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

public class KafkaConsumerService(IElasticSearchService elasticSearchService, IConfiguration configuration) : IKafkaConsumerService
{
    private readonly string _bootstrapServers = configuration.GetSection("Kafka").Get<ConsumerConfig>()!.BootstrapServers;
    private readonly IElasticSearchService _elasticSearchService = elasticSearchService;

    public async Task<TResponse> ConsumeAsync<TResponse>(string topic, CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "permission-consumer-group",
            BootstrapServers = _bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(topic);

            while (true)
            {
                ConsumeResult<Ignore, string> consumeResult = consumer.Consume(cancellationToken);
                var permission = JsonSerializer.Deserialize<Permission>(consumeResult.Message.Value);

                if (permission != null)
                {
                    await _elasticSearchService.IndexPermission(permission, cancellationToken);
                }
            }
        }
    }
}