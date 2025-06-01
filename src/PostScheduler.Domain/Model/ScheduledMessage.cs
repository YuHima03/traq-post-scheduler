namespace PostScheduler.Domain.Model
{
    public sealed record class ScheduledMessage(
        Guid Id,
        Guid UserId,
        Guid ChannelId,
        string Message,
        bool IsEmbeddingEnabled,
        DateTimeOffset ScheduledPostingTime,
        PostedMessageAbstraction? PostedMessage,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt
        );
}
