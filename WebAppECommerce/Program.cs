

using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;

var builder = WebApplication.CreateBuilder(args);
// Configue services or Register Services to container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
//configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapStaticAssets();

app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();

await app.RunAsync();

