using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication5.Data;
using WebApplication5.Data.Services;
using WebApplication5.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");*/
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));*/
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();*/
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");


app.MapControllerRoute(
	name: "area",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "MyArea",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

});
/*app.MapRazorPages();*/
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
