using PostScheduler.Domain.Model;

namespace PostScheduler.Domain.Repository
{
    public interface IScheduledMessagesRepository : IRepositoryBase
    {
        public ValueTask<ScheduledMessage> GetScheduledMessageAsync(Guid id, CancellationToken ct);

        public ValueTask<ScheduledMessage[]> GetUserScheduledMessagesAsync(Guid userId, bool includePosted, CancellationToken ct);

        public ValueTask<ScheduledMessage> PostUserScheduledMessageAsync(PostScheduledMessageRequest request, CancellationToken ct);

        public ValueTask<ScheduledMessage> UpdateScheduledMessageAsync(Guid id, UpdateScheduledMessageRequest request, CancellationToken ct);
    }
}
