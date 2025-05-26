using PostScheduler.Domain.Model;

namespace PostScheduler.Domain.Repository
{
    public interface ITraqAccessTokensRepository : IRepositoryBase
    {
        public ValueTask DeleteTraqAccessTokens(Guid[] userIds, CancellationToken ct);

        public ValueTask<TraqAccessToken?> GetUserAccessTokenAsync(Guid userId, CancellationToken ct);

        public ValueTask<TraqAccessToken?> SaveUserAccessTokenAsync(TraqAccessToken token, CancellationToken ct);
    }
}
