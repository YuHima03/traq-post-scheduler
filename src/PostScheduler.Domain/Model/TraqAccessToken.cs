namespace PostScheduler.Domain.Model
{
    public sealed record class TraqAccessToken(
        Guid UserId,
        string AccessToken,
        DateTimeOffset IssuedAt,
        DateTimeOffset ExpiresAt
        );
}
