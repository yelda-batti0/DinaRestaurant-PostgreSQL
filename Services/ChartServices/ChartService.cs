using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ChartDtos;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ChartServices
{
    public class ChartService : IChartService
    {
        private readonly AppDbContext _context;
        public ChartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationChartDto>> GetLast7DaysReservationCountAsync()
        {
            var today = DateTime.UtcNow.Date;
            var endDate = today.AddDays(6); // bugün dahil sonraki 7 gün

            var reservations = await _context.Reservations
                .Where(r => r.ReservationDate.Date >= today && r.ReservationDate.Date <= endDate)
                .GroupBy(r => r.ReservationDate.Date)
                .Select(g => new ReservationChartDto
                {
                    Day = g.Key.ToString("dd MMM"),
                    Count = g.Count()
                })
                .ToListAsync();

            // Rezervasyon olmayan günler için 0 ile doldur
            var result = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    var date = today.AddDays(i);
                    var label = date.ToString("dd MMM");
                    var found = reservations.FirstOrDefault(r => r.Day == label);
                    return new ReservationChartDto
                    {
                        Day = label,
                        Count = found?.Count ?? 0
                    };
                })
                .ToList();

            return result;
        }

        public async Task<List<CategoryProductCountChartDto>> GetCategoryProductCountAsync()
        {
            var result = await _context.Categories
                .Where(c => c.CategoryStatus)
                .Select(c => new CategoryProductCountChartDto
                {
                    CategoryName = c.CategoryName,
                    ProductCount = c.Products.Count(p => p.Status)
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<CategoryAvgPriceChartDto>> GetCategoryAvgPriceAsync()
        {
            var result = await _context.Categories
                .Where(c => c.CategoryStatus)
                .Select(c => new CategoryAvgPriceChartDto
                {
                    CategoryName = c.CategoryName,
                    AvgPrice = c.Products
                        .Where(p => p.Status)
                        .Any()
                            ? Math.Round((decimal)c.Products
                                .Where(p => p.Status)
                                .Average(p => (double)p.Price), 2)
                            : 0
                })
                .OrderByDescending(c => c.AvgPrice)
                .ToListAsync();

            return result;
        }
    }
}