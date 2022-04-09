namespace TicTacToe.Common
{
    public static class DateTimeProvider
    {
        internal static IDateTimeProvider Provider { get; set; } = new DefaultDateTimeProvider();
        public static DateTime UtcNow => Provider.UtcNow;
    }


    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }


    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}