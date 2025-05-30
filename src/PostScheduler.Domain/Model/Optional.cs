namespace PostScheduler.Domain.Model
{
    public readonly struct Optional<T>()
    {
        public T Value { get; private init; } = default!;

        public bool HasValue { get; private init; } = false;

        public static Optional<T> None => new() { Value = default!, HasValue = false };

        public static Optional<T> Create(T value) => new() { Value = value, HasValue = true };
    }
}
