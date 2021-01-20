using System;

namespace OsirisTest.Utilities.Extensions
{
    public static class DateExtensions
    {
        public static DateTime GetRandomDate(this DateTime dateTime, Random random)
        {
            int wagerIndicator = random.Next(1, 5);

            switch (wagerIndicator)
            {
                case 1:
                case 2:
                    return dateTime.AddMinutes(-random.Next(1, 10));
                case 3:
                    return dateTime.AddHours(-random.Next(1, 23));
                case 4:
                case 5:
                    return dateTime.AddDays(-random.Next(1, 10));
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
