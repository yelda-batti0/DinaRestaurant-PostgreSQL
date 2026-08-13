using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.ViewComponents.MenuViewComponents
{
    public class _MenuListComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;
        public _MenuListComponentPartial(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Products.Include(x => x.Category).OrderBy(y => y.ProductId).ToList();
            return View(values);
        }
    }
}