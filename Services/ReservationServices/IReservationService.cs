using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReservationDtos;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ReservationServices
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservationsAsync();
        Task<GetReservationByIdDto> GetReservationByIdAsync(int id);
        Task CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task DeleteReservationAsync(int id);
        Task ChangeReservationStatusToPending(int id);
        Task ChangeReservationStatusToApprove(int id);
        Task ChangeReservationStatusToCancel(int id);
    }
}
