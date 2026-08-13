using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.StatisticsViewComponents
{
    public class _StatisticsCategoryTableComponentPartial:ViewComponent
    {
        private readonly AppDbContext _context;
        public _StatisticsCategoryTableComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories
                .Where(c => c.CategoryStatus)
                .Select(c => new
                {
                    CategoryName = c.CategoryName,
                    ProductCount = c.Products.Count(p => p.Status),
                    AvgPrice = c.Products.Any(p => p.Status)
                        ? Math.Round(c.Products.Where(p => p.Status).Average(p => (double)p.Price), 0)
                        : 0
                })
                .OrderByDescending(c => c.ProductCount)
                .ToList();

            var maxCount = categories.Any() ? categories.Max(c => c.ProductCount) : 1;

            var result = categories.Select(c => new
            {
                c.CategoryName,
                c.ProductCount,
                c.AvgPrice,
                Percent = maxCount > 0 ? (int)Math.Round((double)c.ProductCount / maxCount * 100) : 0
            }).Take(5).ToList();

            ViewBag.Categories = result;
            return View();
        }
    }
}
