using Microsoft.EntityFrameworkCore;
using PostScheduler.Domain.Repository;

namespace PostScheduler.Infrastructure.Repository
{
    public sealed class RepositoryFactory(IDbContextFactory<Repository> factory) : IRepositoryFactory
    {
        public async ValueTask<IRepository> CreateRepositoryAsync(CancellationToken ct)
        {
            return await factory.CreateDbContextAsync(ct);
        }
    }
}
