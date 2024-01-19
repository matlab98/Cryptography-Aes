namespace CryptographyAes.WebApi.Utilities;

public class FormatDateTime
{
    public static DateTime DateNow(DateTime utcTime)
    {
        TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

        DateTime custDateTime = TimeZoneInfo.ConvertTime(utcTime, tz);
        return custDateTime;
    }
}

