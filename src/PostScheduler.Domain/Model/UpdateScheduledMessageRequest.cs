namespace PostScheduler.Domain.Model
{
    public sealed record class UpdateScheduledMessageRequest(
        Optional<Guid> UserId,
        Optional<string> Message,
        Optional<bool> IsEmbeddingEnabled,
        Optional<DateTimeOffset> ScheduledPostingTime
        )
    {
        public static readonly UpdateScheduledMessageRequest Empty = new(
            Optional<Guid>.None,
            Optional<string>.None,
            Optional<bool>.None,
            Optional<DateTimeOffset>.None
        );
    }
}
