namespace PostScheduler.Domain.Repository
{
    public interface IRepository :
        IRepositoryBase,
        IScheduledMessagesRepository,
        ITraqAccessTokensRepository;

    public interface IRepositoryBase : IAsyncDisposable, IDisposable;
}
