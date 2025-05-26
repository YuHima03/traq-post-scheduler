namespace PostScheduler.Domain.Model
{
    public readonly struct PostedMessageAbstraction
    {
        public Guid MessageId { get; init; }
        public DateTimeOffset PostedAt { get; init; }
    }
}
