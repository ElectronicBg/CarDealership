using CarDealership.Data;
using CarDealership.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


/*builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "getmodels",
        pattern: "Car/GetModels/{brandId}",
        defaults: new { controller = "Car", action = "GetModels" });

    endpoints.MapControllerRoute(
      name: "Search",
      pattern: "Car/Search",
      defaults: new { controller = "Car", action = "Search" }
  );
    endpoints.MapControllerRoute(
        name: "Details",
        pattern: "Car/Details/{id}",
        defaults: new { controller = "Car", action = "Details" });

    endpoints.MapControllerRoute(
    name: "DeletePhoto",
    pattern: "Photo/Delete/{photoId}",
    defaults: new { controller = "Photo", action = "Delete" }
);

    endpoints.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.MapRazorPages();

//Seed Roles
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);

    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

    var dbSeeder = new DbSeeder(); 

    await dbSeeder.SeedBrands(dbContext);
    await dbSeeder.SeedModels(dbContext);
    await dbSeeder.SeedCarColors(dbContext);
    await dbSeeder.SeedCars(dbContext);
    await dbSeeder.SeedPhotos(dbContext);
}

app.Run();
