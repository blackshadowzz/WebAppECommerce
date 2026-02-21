

var builder = WebApplication.CreateBuilder(args);
// Configue services or Register Services to container

builder.Services.AddControllersWithViews();


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

app.Run();

