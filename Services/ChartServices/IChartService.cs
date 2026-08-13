using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ChartDtos;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ChartServices
{
    public interface IChartService
    {
        Task<List<ReservationChartDto>> GetLast7DaysReservationCountAsync();
        Task<List<CategoryProductCountChartDto>> GetCategoryProductCountAsync();
        Task<List<CategoryAvgPriceChartDto>> GetCategoryAvgPriceAsync();
    }
}
