namespace ChallengeN5.Query.Domain.Application.Service;


public interface IKafkaConsumerService
{
    /// <summary>
    /// Consumes a message from a topic
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse> ConsumeAsync<TResponse>(string topic, CancellationToken cancellationToken);
}
