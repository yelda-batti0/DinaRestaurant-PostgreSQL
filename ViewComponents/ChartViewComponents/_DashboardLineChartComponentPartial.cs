using DatabaseMastery.DinnerMenuPostgreSQL.Services.ChartServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.ChartViewComponents
{
    public class _DashboardLineChartComponentPartial:ViewComponent
    {
        private readonly IChartService _chartService;

        public _DashboardLineChartComponentPartial(IChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _chartService.GetLast7DaysReservationCountAsync();
            return View(values);
        }
    }
}
