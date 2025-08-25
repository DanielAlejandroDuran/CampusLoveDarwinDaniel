namespace CampusLoveDarwinDaniel.Shared.Helpers
{
    public static class DateHelper
    {
        public static bool IsSameDay(DateTime date1, DateTime date2)
        {
            return date1.Date == date2.Date;
        }
    }
}
