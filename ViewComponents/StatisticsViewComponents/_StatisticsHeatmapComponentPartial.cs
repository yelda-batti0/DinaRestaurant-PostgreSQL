using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsHeatmapComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsHeatmapComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var hourSlots = new[] { 12, 14, 16, 18, 20, 22 };

            var raw = _context.Reservations
                .Select(r => new { r.ReservationDate, r.ReservationTime })
                .AsNoTracking()
                .ToList();                       // buradan sonrası C# tarafında

            var reservations = raw
                .Select(r => new
                {
                    DayOfWeek = r.ReservationDate.DayOfWeek,
                    Hour = r.ReservationTime.Hours
                })
                .ToList();

            // [saat index, gün index] → sayı
            // Gün: 0=Pzt, 1=Sal, 2=Çar, 3=Per, 4=Cum, 5=Cmt, 6=Paz
            var heatData = new int[6, 7];

            foreach (var r in reservations)
            {
                // Saat dilimini bul (en yakın slot)
                int hourIndex = -1;
                for (int i = 0; i < hourSlots.Length; i++)
                {
                    if (r.Hour >= hourSlots[i] && (i == hourSlots.Length - 1 || r.Hour < hourSlots[i + 1]))
                    {
                        hourIndex = i;
                        break;
                    }
                }
                if (hourIndex == -1) continue;

                // DayOfWeek → Pazartesi bazlı index
                int dayIndex = r.DayOfWeek switch
                {
                    DayOfWeek.Monday => 0,
                    DayOfWeek.Tuesday => 1,
                    DayOfWeek.Wednesday => 2,
                    DayOfWeek.Thursday => 3,
                    DayOfWeek.Friday => 4,
                    DayOfWeek.Saturday => 5,
                    DayOfWeek.Sunday => 6,
                    _ => -1
                };
                if (dayIndex == -1) continue;

                heatData[hourIndex, dayIndex]++;
            }

            // Renk normalize etmek için max değeri bul
            int maxVal = 1;
            for (int h = 0; h < 6; h++)
                for (int d = 0; d < 7; d++)
                    if (heatData[h, d] > maxVal) maxVal = heatData[h, d];

            ViewBag.HeatData = heatData;
            ViewBag.MaxVal = maxVal;

            return View();
        }
    }
}
