namespace PostScheduler.Domain.Repository
{
    public interface IRepositoryFactory
    {
        public ValueTask<IRepository> CreateRepositoryAsync(CancellationToken ct);
    }
}
