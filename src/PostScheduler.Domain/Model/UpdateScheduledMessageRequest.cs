namespace PostScheduler.Domain.Model
{
    public sealed record class UpdateScheduledMessageRequest(
        Optional<Guid> UserId,
        Optional<string> Message,
        Optional<bool> IsEmbeddingEnabled,
        Optional<DateTimeOffset> ScheduledPostingTime
        );
}
