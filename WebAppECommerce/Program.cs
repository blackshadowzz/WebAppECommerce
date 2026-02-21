

var builder = WebApplication.CreateBuilder(args);
// Configue services or Register Services to container

builder.Services.AddControllersWithViews();



var app = builder.Build();
//configure the HTTP request pipeline

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();

app.MapStaticAssets();

//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"

    ).WithStaticAssets();

app.Run();

