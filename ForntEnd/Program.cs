using Microsoft.Extensions.Options;
using MVC_web.Models;
using MVC_web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<Assessment2DatabaseSetting>(
                builder.Configuration.GetSection(nameof(Assessment2DatabaseSetting)));

builder.Services.AddSingleton<IAssessment2DatabaseSetting>(sp =>
sp.GetRequiredService<IOptions<Assessment2DatabaseSetting>>().Value);


builder.Services.AddScoped<IPlayersServices, PlayersServices>();
builder.Services.AddScoped<ICharactersServices, CharactersServices>();


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
