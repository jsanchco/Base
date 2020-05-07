namespace SGI.Domain.Helpers
{
    #region Using

    using System;

    #endregion

    public static class DateTimeExtension
    {
        public static string ToStringES(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return ((DateTime)dateTime).ToString("dd/MM/yyyy");
        }

        public static string ToStringEU(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return ((DateTime)dateTime).ToString("MM/dd/yyyy");
        }

        public static DateTime? RemoveTime(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            var date = ((DateTime)dateTime).ToLocalTime();
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
