using AutoMapper;
using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReservationDtos;
using DatabaseMastery.DinnerMenuPostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ReservationServices
{
        public class ReservationService : IReservationService
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            public ReservationService(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

        public async Task ChangeReservationStatusToApprove(int id)
        {
            var value = await _context.Reservations.FindAsync(id);
            value.Status = "Onaylandi";
            await _context.SaveChangesAsync();
        }

        public async Task ChangeReservationStatusToCancel(int id)
        {
            var value = await _context.Reservations.FindAsync(id);
            value.Status = "Iptal Edildi";
            await _context.SaveChangesAsync();
        }

        public async Task ChangeReservationStatusToPending(int id)
        {
            var value = await _context.Reservations.FindAsync(id);
            value.Status = "Beklemede";
            await _context.SaveChangesAsync();
        }

        public async Task CreateReservationAsync(CreateReservationDto createReservationDto)
            {
                var value = _mapper.Map<Reservation>(createReservationDto);
                await _context.Reservations.AddAsync(value);
                await _context.SaveChangesAsync();
            }
            public async Task DeleteReservationAsync(int id)
            {
                var value = await _context.Reservations.FindAsync(id);
                _context.Reservations.Remove(value);
                await _context.SaveChangesAsync();
            }
            public async Task<List<ResultReservationDto>> GetAllReservationsAsync()
            {
            var values = await _context.Reservations
            .OrderBy(x => x.ReservationId)
            .ToListAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }
            public async Task<GetReservationByIdDto> GetReservationByIdAsync(int id)
            {
                var value = await _context.Reservations.FindAsync(id);
                return _mapper.Map<GetReservationByIdDto>(value);
            }
            public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
            {
                var value = _mapper.Map<Reservation>(updateReservationDto);
                _context.Reservations.Update(value);
                await _context.SaveChangesAsync();
            }
        }
}
