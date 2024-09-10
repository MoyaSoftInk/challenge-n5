namespace ChallengeN5.Query.Domain.Application.Service;

using ChallengeN5.Query.Domain.Application.Model;

public interface IKafkaConsumerService
{
    /// <summary>
    /// Consumes a message from a topic
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Permission?> ConsumeAsync(string topic, CancellationToken cancellationToken);
}
