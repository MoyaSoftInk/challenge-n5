namespace ChallengeN5.Query.Infrastructure.Application.Service.BackGroundService;

using ChallengeN5.Query.Domain.Application.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class KafkaConsumerBackgroundService : BackgroundService
{
    private readonly IKafkaConsumerService _kafkaConsumerService;
    private readonly ILogger<KafkaConsumerBackgroundService> _logger;

    public KafkaConsumerBackgroundService(IKafkaConsumerService kafkaConsumerService, ILogger<KafkaConsumerBackgroundService> logger)
    {
        _kafkaConsumerService = kafkaConsumerService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka consumer background service is starting.");

        stoppingToken.Register(() => _logger.LogInformation("Kafka consumer background service is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var response = await _kafkaConsumerService.ConsumeAsync("permission-topic", stoppingToken);

                _logger.LogInformation("Message consumed: {0}", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming a message from Kafka.");
            }

            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("Kafka consumer background service is stopping.");
    }
}
