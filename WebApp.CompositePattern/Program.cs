using Base.Main.Models;
using Base.Main.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.CompositePattern.Models;

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
        var mainUser = new Customer()
        {
            UserName = "Alparslan",
            Email = "alparslan1@gmail.com"
        };
        
        userManager.CreateAsync(mainUser, "A.lparslan123").Wait();
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


        //

        var category1 = new Category()
        {
            Name = "Roman Kitaplarý",
            ReferenceId = 0,
            UserId = mainUser.Id
        };

        var category2 = new Category()
        {
            Name = "Suç Kitaplarý",
            ReferenceId = 0,
            UserId = mainUser.Id
        };

        var category3 = new Category()
        {
            Name = "Dram Kitaplarý",
            ReferenceId = 0,
            UserId = mainUser.Id
        };

        identityDbContext.Categories.AddRange(category1,category2,category3);
        identityDbContext.SaveChanges();

        var subCategory = new Category()
        {
            Name = "Suç Kitaplarý Episode 2",
            ReferenceId = category2.Id,
            UserId = mainUser.Id
        };

        var subCategory2 = new Category()
        {
            Name = "Roman Kitaplarý Episode 2",
            ReferenceId = category1.Id,
            UserId = mainUser.Id
        };

        var subCategory3 = new Category()
        {
            Name = "Dram Kitaplarý Episode 2",
            ReferenceId = category3.Id,
            UserId = mainUser.Id
        };

        identityDbContext.AddRange(subCategory, subCategory2, subCategory3);
        identityDbContext.SaveChanges();goto SUB;
        

        SUB:
        var subCategory4 = new Category()
        {
            Name = "Suç Kitaplarý Episode 2.1",
            ReferenceId = subCategory.Id,
            UserId = mainUser.Id
        };
        identityDbContext.Add(subCategory4);
        identityDbContext.SaveChanges();

        //

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
