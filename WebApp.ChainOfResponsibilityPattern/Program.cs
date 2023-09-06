using Base.Main.Models;
using Base.Main.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.ChainOfResponsibilityPattern.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});

builder.Services.AddIdentity<Customer,IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
  .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
    identityDbContext.Database.Migrate();
    if (!userManager.Users.Any())
    {
        userManager.CreateAsync(new Customer()
        {
            UserName="Alparslan", Email="alparslan1@gmail.com"
        }, "A.lparslan123").Wait();

        userManager.CreateAsync(new Customer()
        {
            UserName = "Kayhan",
            Email = "kayhan1@gmail.com"
        }, "A.lparslan123").Wait();

        userManager.CreateAsync(new Customer()
        {
            UserName = "Ramo",
            Email = "ramo@gmail.com"
        }, "A.lparslan123").Wait();

    }

    if (!identityDbContext.Products.Any())
    {
        Enumerable.Range(1, 20).ToList().ForEach(x =>
        {
            identityDbContext.Products.Add(new Product()
            {
                Name = $"Bilgisayar{x}",
                Price = 14000,
                Stock = 200
            });
        });
        identityDbContext.SaveChanges();
    }
}

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
