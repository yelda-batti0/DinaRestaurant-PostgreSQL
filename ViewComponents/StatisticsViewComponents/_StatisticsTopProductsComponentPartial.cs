using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsTopProductsComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsTopProductsComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var topProducts = _context.Reviews
                .GroupBy(r => r.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ReviewCount = g.Count(),
                    AvgRating = Math.Round(g.Average(r => r.Rating), 1)
                })
                .OrderByDescending(x => x.ReviewCount)
                .Take(5)
                .Join(_context.Products,
                      r => r.ProductId,
                      p => p.ProductId,
                      (r, p) => new
                      {
                          p.ProductId,
                          p.ProductName,
                          r.ReviewCount,
                          AvgRating = r.AvgRating.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)
                      })
                .ToList();

            ViewBag.TopProducts = topProducts;
            return View();
        }
    }
}
