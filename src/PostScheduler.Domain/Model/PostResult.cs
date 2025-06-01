namespace PostScheduler.Domain.Model
{
    public sealed class PostResult
    {
        readonly string? _errorMessage;
        readonly PostedMessageAbstraction _postedMessageAbstraction;

        public string ErrorMessage
        {
            get
            {
                if (IsSuccessful)
                {
                    throw new InvalidOperationException("The post was successful, no error message is available.");
                }
                return _errorMessage!;
            }
        }

        public bool IsSuccessful => _errorMessage is null;

        public PostedMessageAbstraction PostedMessageAbstraction
        {
            get
            {
                if (!IsSuccessful)
                {
                    throw new InvalidOperationException("The post was not successful.");
                }
                return _postedMessageAbstraction;
            }
        }

        PostResult(PostedMessageAbstraction postedMessageAbstraction)
        {
            _postedMessageAbstraction = postedMessageAbstraction;
            _errorMessage = null;
        }

        PostResult(string errorMessage)
        {
            ArgumentNullException.ThrowIfNull(errorMessage);
            _postedMessageAbstraction = default;
            _errorMessage = errorMessage;
        }

        public static PostResult Success(PostedMessageAbstraction postedMessageAbstraction) => new(postedMessageAbstraction);

        public static PostResult Failure(string errorMessage) => new(errorMessage);
    }

    public readonly struct PostedMessageAbstraction
    {
        public required Guid MessageId { get; init; }
        public required DateTimeOffset PostedAt { get; init; }
    }
}
