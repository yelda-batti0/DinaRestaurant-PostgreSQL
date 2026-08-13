using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsReservationStatusComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsReservationStatusComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var total = _context.Reservations.Count();
            var approved = _context.Reservations.Count(r => r.Status == "Onaylandı");
            var pending = _context.Reservations.Count(r => r.Status == "Beklemede");
            var cancelled = _context.Reservations.Count(r => r.Status == "İptal Edildi");

            ViewBag.total = total;
            ViewBag.approved = approved;
            ViewBag.pending = pending;
            ViewBag.cancelled = cancelled;

            //ViewBag.approvedRate = total > 0 ? Math.Round((double)approved / total * 100, 1) : 0;
            //ViewBag.pendingRate = total > 0 ? Math.Round((double)pending / total * 100, 1) : 0;
            //ViewBag.cancelledRate = total > 0 ? Math.Round((double)cancelled / total * 100, 1) : 0;

            ViewBag.approvedRate = total > 0 ? Math.Round((double)approved / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.pendingRate = total > 0 ? Math.Round((double)pending / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.cancelledRate = total > 0 ? Math.Round((double)cancelled / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";

            return View();
        }
    }
}
