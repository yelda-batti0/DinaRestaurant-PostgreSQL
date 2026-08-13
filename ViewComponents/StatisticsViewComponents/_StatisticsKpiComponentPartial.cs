using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsKpiComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsKpiComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            // Rezervasyon istatistikleri
            ViewBag.totalReservation = _context.Reservations.Count();
            ViewBag.approvedReservation = _context.Reservations.Count(r => r.Status == "Onaylandı");
            ViewBag.pendingReservation = _context.Reservations.Count(r => r.Status == "Beklemede");
            ViewBag.cancelledReservation = _context.Reservations.Count(r => r.Status == "İptal Edildi");

            // Değerlendirme istatistikleri
            ViewBag.totalReview = _context.Reviews.Count();
            ViewBag.avgRating = _context.Reviews.Any() ? Math.Round(_context.Reviews.Average(r => r.Rating), 1) : 0;
            ViewBag.fiveStarReview = _context.Reviews.Count(r => r.Rating == 5);
            var totalReview = _context.Reviews.Count();
            ViewBag.fiveStarPercent = totalReview > 0 ? Math.Round((double)ViewBag.fiveStarReview / totalReview * 100, 1) : 0;

            // Ürün & müşteri
            ViewBag.totalProduct = _context.Products.Count(p => p.Status);
            ViewBag.categoryCount = _context.Categories.Count(c => c.CategoryStatus);
            ViewBag.totalCustomer = _context.Reservations.Sum(r => r.GuestCount);

            // Onay oranı
            ViewBag.approvalRate = ViewBag.totalReservation > 0
                ? Math.Round((double)ViewBag.approvedReservation / ViewBag.totalReservation * 100, 1)
                : 0;
            ViewBag.cancelRate = ViewBag.totalReservation > 0
                ? Math.Round((double)ViewBag.cancelledReservation / ViewBag.totalReservation * 100, 1)
                : 0;
            return View();
        }
    }
}
