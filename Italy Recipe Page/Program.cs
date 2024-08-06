using FluentAssertions.Common;
using Italy_Recipe_Page.Repository;
using Italy_Recipe_Page.Models;
using Microsoft.EntityFrameworkCore;
using Italy_Recipe_Page.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IVnPayService, VnPayService>();
var connect = builder.Configuration.GetConnectionString("ItalyContext");
builder.Services.AddDbContext<ItalyContext>(x => x.UseSqlServer(connect));
builder.Services.AddScoped<RecipeCategoryInterface, RecipeCatRepo>();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(60);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();