namespace Filtery.Builders.ExpressionValueConverters.Concrete
{
    public class TimeSpanDuration
    {
        public long Ticks { get; }
        public TimeSpanDuration(long ticks) => Ticks = ticks;
        public static implicit operator TimeSpanDuration(long t) => new TimeSpanDuration(t);
        public static implicit operator long(TimeSpanDuration d) => d.Ticks;
    }
}