

using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;

var builder = WebApplication.CreateBuilder(args);
// Configue services or Register Services to container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSqlServer"));
});

var app = builder.Build();
//configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();

app.MapStaticAssets();

//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();

await app.RunAsync();

