using Microsoft.EntityFrameworkCore;

namespace PostScheduler.Infrastructure.Repository
{
    public sealed partial class Repository : Domain.Repository.IScheduledMessagesRepository
    {
        public async ValueTask<Domain.Model.ScheduledMessage> GetScheduledMessageAsync(Guid id, CancellationToken ct)
        {
            var sm = await ScheduledMessages
                .Where(m => m.Id == id)
                .SingleAsync(ct);
            return sm.ToDomainModel();
        }

        public async ValueTask<Domain.Model.ScheduledMessage[]> GetUserScheduledMessagesAsync(Guid userId, bool includePosted, CancellationToken ct)
        {
            var filter = ScheduledMessages.Where(m => m.UserId == userId);
            if (!includePosted)
            {
                filter = filter.Where(m => m.PostedMessageId == null);
            }
            return await filter
                .Select(m => m.ToDomainModel())
                .ToArrayAsync(ct);
        }

        public async ValueTask<Domain.Model.ScheduledMessage> PostUserScheduledMessageAsync(Domain.Model.PostScheduledMessageRequest request, CancellationToken ct)
        {
            Model.ScheduledMessage sm = new()
            {
                Id = Guid.CreateVersion7(),
                UserId = request.UserId,
                Message = request.Message,
                IsEmbeddingEnabled = request.IsEmbeddingEnabled,
                ScheduledPostingTime = request.ScheduledPostingTime.UtcDateTime,
            };
            if (request.PostedMessage is not null)
            {
                sm.PostedAt = request.PostedMessage.Value.PostedAt.UtcDateTime;
                sm.PostedMessageId = request.PostedMessage.Value.MessageId;
            }
            ScheduledMessages.Add(sm);
            await SaveChangesAsync(ct);
            return sm.ToDomainModel();
        }

        public async ValueTask<Domain.Model.ScheduledMessage> UpdateScheduledMessageAsync(Guid id, Domain.Model.UpdateScheduledMessageRequest request, CancellationToken ct)
        {
            var sm = await ScheduledMessages
                .Where(m => m.Id == id)
                .SingleAsync(ct);

            if (request.UserId.HasValue && sm.UserId != request.UserId.Value)
            {
                sm.UserId = request.UserId.Value;
            }
            if (request.Message.HasValue && sm.Message != request.Message.Value)
            {
                sm.Message = request.Message.Value;
            }
            if (request.IsEmbeddingEnabled.HasValue && sm.IsEmbeddingEnabled != request.IsEmbeddingEnabled.Value)
            {
                sm.IsEmbeddingEnabled = request.IsEmbeddingEnabled.Value;
            }
            if (request.ScheduledPostingTime.HasValue && sm.ScheduledPostingTime != request.ScheduledPostingTime.Value.UtcDateTime)
            {
                sm.ScheduledPostingTime = request.ScheduledPostingTime.Value.UtcDateTime;
            }
            if (request.PostedMessage.HasValue)
            {
                var pm = request.PostedMessage.Value;
                if (pm is null)
                {
                    sm.PostedAt = null;
                    sm.PostedMessageId = null;
                }
                else
                {
                    sm.PostedAt = pm.Value.PostedAt.UtcDateTime;
                    sm.PostedMessageId = pm.Value.MessageId;
                }
            }

            await SaveChangesAsync(ct);
            return sm.ToDomainModel();
        }
    }
}
