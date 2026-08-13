using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.CategoryServices;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.ProductServices;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.ReservationServices;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.ChartServices;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.DashboardServices;
using DatabaseMastery.DinnerMenuPostgreSQL.Services.ReviewServices;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
