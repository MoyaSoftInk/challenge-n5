namespace ChallengeN5.Command.Infrastructure.Application.Service;

using ChallengeN5.Query.Domain.Application.Model;
using ChallengeN5.Query.Domain.Application.Service;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

public class KafkaConsumerService(IElasticSearchService elasticSearchService, IConfiguration configuration, ILogger<KafkaConsumerService> logger) : IKafkaConsumerService
{
    private readonly string _bootstrapServers = configuration.GetSection("Kafka").Get<ConsumerConfig>()!.BootstrapServers;
    private readonly IElasticSearchService _elasticSearchService = elasticSearchService;
    private readonly ILogger<KafkaConsumerService> _logger = logger;

    public async Task<Permission?> ConsumeAsync(string topic, CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "permission-consumer-group",
            BootstrapServers = _bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        await TopicExistsOrCreateAsync(_bootstrapServers, topic, 1, 1);

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(topic);

            ConsumeResult<Ignore, string> consumeResult = consumer.Consume(cancellationToken);
            var permission = JsonSerializer.Deserialize<Permission>(consumeResult.Message.Value);

            if (permission != null)
            {
                await _elasticSearchService.IndexPermission(permission, cancellationToken);
            }
            return permission;
        }
    }

    private async Task TopicExistsOrCreateAsync(string bootstrapServers, string topicName, int numPartitions, short replicationFactor)
    {
        var config = new AdminClientConfig { BootstrapServers = bootstrapServers };

        using (var adminClient = new AdminClientBuilder(config).Build())
        {
            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));

                if (metadata.Topics.Any(t => t.Topic == topicName))
                {
                    _logger.LogInformation($"Topic {topicName} already exists.");
                    return;
                }

                var topicSpecification = new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = numPartitions,
                    ReplicationFactor = replicationFactor
                };

                await adminClient.CreateTopicsAsync(new[] { topicSpecification });
                _logger.LogInformation($"Topic {topicName} created successfully.");
            }
            catch (CreateTopicsException e)
            {
                _logger.LogError($"Error creating topic {topicName}: {e.Results[0].Error.Reason}");
            }
            catch (KafkaException ex)
            {
                _logger.LogError($"Error checking or creating topic {topicName}: {ex.Message}");
            }
        }
    }

}