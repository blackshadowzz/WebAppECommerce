

using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Services;

var builder = WebApplication.CreateBuilder(args);
// Configue services or Register Services to container
// Add services to the container.
builder.Services.AddControllersWithViews();

// Inject OrderService
builder.Services.AddScoped<IOrderService, OrderService>(); // Add this line to register the OrderService

// Add session services for managing user sessions and storing order information temporarily
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
//configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Point to your Error route
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapStaticAssets();

app.UseRouting();
app.UseSession(); // Enable session middleware

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();

app.Run();

