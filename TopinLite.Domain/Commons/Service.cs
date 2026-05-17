using System.Globalization;

namespace TopinLite.Domain.Commons;

public static class Service
{
    public static DateTime ParseJalaliDateTime(string jalaliDate, string time)
    {
        // jalaliDate: "1403/09/15"  →  year=1403, month=9, day=15
        // time:       "14:35:22"    →  hour=14, min=35, sec=22

        string[] dateParts = jalaliDate.Split('/');
        string[] timeParts = time.Split(':');

        int year = int.Parse(dateParts[0]);
        int month = int.Parse(dateParts[1]);
        int day = int.Parse(dateParts[2]);
        int hour = int.Parse(timeParts[0]);
        int minute = int.Parse(timeParts[1]);
        int second = int.Parse(timeParts[2]);

        var pc = new PersianCalendar();
        return pc.ToDateTime(year, month, day, hour, minute, second, millisecond: 0);
    }
}
