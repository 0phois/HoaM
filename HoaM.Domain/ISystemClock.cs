namespace HoaM.Domain
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }

    public sealed class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow { get; } = DateTimeOffset.UtcNow;
    }
}
