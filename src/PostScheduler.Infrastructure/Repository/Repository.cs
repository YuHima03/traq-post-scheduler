using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace PostScheduler.Infrastructure.Repository
{
    public sealed partial class Repository(DbContextOptions options, ConcurrentDictionary<Guid, Domain.Model.TraqAccessToken> accessTokensContainer) : DbContext(options), Domain.Repository.IRepository
    {
        DbSet<Model.ScheduledMessage> ScheduledMessages { get; set; }
    }
}
