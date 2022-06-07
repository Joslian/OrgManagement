using System;

namespace OrgManagement.Helpers
{
    internal class DateTimeConverter
    {
        public static double CalcVacationDays(DateTime? createdAt)
        {
            if (createdAt.HasValue)
            {
                var now = DateTime.Now;

                var delta = (now.Month + now.Year * 12) - (createdAt.Value.Month + createdAt.Value.Year * 12);

                return Math.Round(delta * 2.33);
            }

            return 0;
        }
    }
}
