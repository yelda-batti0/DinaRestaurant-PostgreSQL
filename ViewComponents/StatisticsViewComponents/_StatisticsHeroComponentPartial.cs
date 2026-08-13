using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsHeroComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsHeroComponentPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.totalReservationCount = _context.Reservations.Count();
            ViewBag.totalCustomerCount = _context.Reservations.Sum(x => x.GuestCount);
            ViewBag.avgScore = _context.Reviews.Average(x => x.Rating);
            ViewBag.totalReviewCount = _context.Reviews.Count();
            ViewBag.activeProduct = _context.Products.Where(x => x.Status == true).Count();
            ViewBag.categoryCount = _context.Categories.Count();

            var today = DateTime.UtcNow.Date;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            ViewBag.thisWeekTotalGuestCount = _context.Reservations
                .Where(r => r.ReservationDate.Date >= startOfWeek
                         && r.ReservationDate.Date < endOfWeek
                         && r.Status == "Onaylandı")
                .Sum(r => r.GuestCount);

            return View();
        }
        }
}
