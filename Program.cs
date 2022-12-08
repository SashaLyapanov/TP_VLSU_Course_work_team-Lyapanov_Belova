using Microsoft.EntityFrameworkCore;
using TravelAgency_Prod.Data;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10000000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = false;
});*/
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();



builder.Services.AddDbContext<TravelAgencyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
