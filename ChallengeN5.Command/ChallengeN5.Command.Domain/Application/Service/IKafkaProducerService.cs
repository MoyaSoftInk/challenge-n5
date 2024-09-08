namespace ChallengeN5.Command.Domain.Application.Service;


public interface IKafkaProducerService
{
    /// <summary>
    /// Post a message to a topic
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ProduceAsync(string topic, object data, CancellationToken cancellationToken);
}
