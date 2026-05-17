namespace TopinLite.Domain.Commons;

public static class PersianDateHelper
{
    private static readonly System.Globalization.PersianCalendar _pc = new();

    public static string ToDate(DateTime date)
    {
        int year = _pc.GetYear(date);
        int month = _pc.GetMonth(date);
        int day = _pc.GetDayOfMonth(date);
        return $"{year:0000}/{month:00}/{day:00}";
    }

    public static string ToTime(DateTime date)
        => date.ToString("HH:mm:ss");

    public static (string Date, string Time) ToDateAndTime(DateTime date)
        => (ToDate(date), ToTime(date));

    public static string ToFirstDayOfMonth(DateTime date)
    {
        int year = _pc.GetYear(date);
        int month = _pc.GetMonth(date);
        return $"{year:0000}/{month:00}/01";
    }
}
