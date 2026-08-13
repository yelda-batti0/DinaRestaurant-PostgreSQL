using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsRatingDistComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsRatingDistComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var total = _context.Reviews.Count();
            var avgRating = total > 0
                ? Math.Round(_context.Reviews.Average(r => r.Rating), 1)
                : 0;

            ViewBag.totalReview = total;
            ViewBag.avgRating = avgRating;

            ViewBag.star5 = _context.Reviews.Count(r => r.Rating == 5);
            ViewBag.star4 = _context.Reviews.Count(r => r.Rating == 4);
            ViewBag.star3 = _context.Reviews.Count(r => r.Rating == 3);
            ViewBag.star2 = _context.Reviews.Count(r => r.Rating == 2);
            ViewBag.star1 = _context.Reviews.Count(r => r.Rating == 1);

            //ViewBag.pct5 = total > 0 ? Math.Round((double)ViewBag.star5 / total * 100, 1) : 0;
            //ViewBag.pct4 = total > 0 ? Math.Round((double)ViewBag.star4 / total * 100, 1) : 0;
            //ViewBag.pct3 = total > 0 ? Math.Round((double)ViewBag.star3 / total * 100, 1) : 0;
            //ViewBag.pct2 = total > 0 ? Math.Round((double)ViewBag.star2 / total * 100, 1) : 0;
            //ViewBag.pct1 = total > 0 ? Math.Round((double)ViewBag.star1 / total * 100, 1) : 0;

            ViewBag.pct5 = total > 0 ? Math.Round((double)ViewBag.star5 / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.pct4 = total > 0 ? Math.Round((double)ViewBag.star4 / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.pct3 = total > 0 ? Math.Round((double)ViewBag.star3 / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.pct2 = total > 0 ? Math.Round((double)ViewBag.star2 / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";
            ViewBag.pct1 = total > 0 ? Math.Round((double)ViewBag.star1 / total * 100, 1).ToString("F1", System.Globalization.CultureInfo.InvariantCulture) : "0";

            return View();
        }
    }
}
