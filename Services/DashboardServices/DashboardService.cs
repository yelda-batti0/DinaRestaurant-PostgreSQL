using AutoMapper;
using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReservationDtos;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DashboardService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> GetApprovedReservationCountAsync()
        {
            return await _context.Reservations.CountAsync(x => x.Status == "Onaylandi");
        }
        public async Task<int> GetCancelledReservationCountAsync()
        {
            return await _context.Reservations.CountAsync(x => x.Status == "Iptal Edildi");
        }
        public async Task<int> GetPendingReservationCountAsync()
        {
            return await _context.Reservations.CountAsync(x => x.Status == "Beklemede");
        }
        public Task<int> GetTodayOrderCountAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetTodayReservationCountAsync()
        {
            return await _context.Reservations.CountAsync(x => x.ReservationDate.Date == DateTime.Today);
        }

        public async Task<List<ResultReservationDto>> GetTodayReservationListAsync()
        {
            var today = DateTime.UtcNow.Date;

            var values = await _context.Reservations
                .Where(r => r.ReservationDate.Date == today)
                .OrderBy(r => r.ReservationTime)
                .ToListAsync();

            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<int> GetTotalCustomerCountAsync()
        {
            return await _context.Reservations.SumAsync(x => x.GuestCount);
        }
        public async Task<int> GetTotalMenuProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalReservationCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }
    }
}
