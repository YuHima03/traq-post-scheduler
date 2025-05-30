namespace PostScheduler.Domain.Model
{
    public sealed record class PostScheduledMessageRequest(
        Guid UserId,
        string Message,
        bool IsEmbeddingEnabled,
        DateTimeOffset ScheduledPostingTime,
        PostedMessageAbstraction? PostedMessage
        );
}
