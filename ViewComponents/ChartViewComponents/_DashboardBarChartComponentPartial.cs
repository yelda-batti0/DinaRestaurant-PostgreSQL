using DatabaseMastery.DinnerMenuPostgreSQL.Services.ChartServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.ChartViewComponents
{
    public class _DashboardBarChartComponentPartial:ViewComponent
    {
        private readonly IChartService _chartService;

        public _DashboardBarChartComponentPartial(IChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var values = await _chartService.GetCategoryProductCountAsync();
            return View(values);
        }
    }
}
