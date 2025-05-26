using System.Collections.Concurrent;

namespace PostScheduler.Infrastructure.Repository
{
    public sealed partial class Repository : Domain.Repository.ITraqAccessTokensRepository
    {
        readonly ConcurrentDictionary<Guid, Domain.Model.TraqAccessToken> _tokens = accessTokensContainer;

        public ValueTask DeleteTraqAccessTokens(Guid[] userIds, CancellationToken ct)
        {
            foreach (var userId in userIds)
            {
                _tokens.Remove(userId, out _);
            }
            return ValueTask.CompletedTask;
        }

        public ValueTask<Domain.Model.TraqAccessToken?> GetUserAccessTokenAsync(Guid userId, CancellationToken ct)
        {
            if (_tokens.TryGetValue(userId, out var token))
            {
                return ValueTask.FromResult<Domain.Model.TraqAccessToken?>(token);
            }
            return ValueTask.FromResult<Domain.Model.TraqAccessToken?>(null);
        }

        public ValueTask<Domain.Model.TraqAccessToken?> SaveUserAccessTokenAsync(Domain.Model.TraqAccessToken token, CancellationToken ct)
        {
            return ValueTask.FromResult<Domain.Model.TraqAccessToken?>(
                _tokens.AddOrUpdate(token.UserId, token, (id, prev) => token)
            );
        }
    }
}
