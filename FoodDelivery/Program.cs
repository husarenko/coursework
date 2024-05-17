using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBAccountsContextConnection") ?? throw new InvalidOperationException("Connection string 'DBAccountsContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PersonContext>(options =>
{
    options.UseSqlServer("Server=WIN-RA69BC6P49V;Database=PersonDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddDbContext<RestaurantContext>(options =>
{
    options.UseSqlServer("Server=WIN-RA69BC6P49V;Database=RestaurantNewDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddDbContext<DishContext>(options =>
{
    options.UseSqlServer("Server=WIN-RA69BC6P49V;Database=DishDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer("Server=WIN-RA69BC6P49V;Database=OrderDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddIdentity<Person, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<PersonContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
