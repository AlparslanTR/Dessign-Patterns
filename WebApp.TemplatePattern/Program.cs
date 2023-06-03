using Base.Main.Models;
using Base.Main.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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
            UserName="Alparslan", 
            Email="alparslan1@gmail.com", 
            ImageUrl= "/UserPictures/registerUser.jpg",
            Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam et maximus ligula, id consequat dui. Nullam id augue non est interdum elementum. Nunc ornare felis id lacus tempor egestas. Morbi purus purus, efficitur in aliquet vitae, faucibus eros."
        }, "A.lparslan123").Wait();

        userManager.CreateAsync(new Customer()
        {
            UserName = "Kayhan",
            Email = "kayhan1@gmail.com",
            ImageUrl = "/UserPictures/registerUser.jpg",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam et maximus ligula, id consequat dui. Nullam id augue non est interdum elementum. Nunc ornare felis id lacus tempor egestas. Morbi purus purus, efficitur in aliquet vitae, faucibus eros."
        }, "A.lparslan123").Wait();

        userManager.CreateAsync(new Customer()
        {
            UserName = "Ramo",
            Email = "ramo@gmail.com",
            ImageUrl = "/UserPictures/registerUser.jpg",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam et maximus ligula, id consequat dui. Nullam id augue non est interdum elementum. Nunc ornare felis id lacus tempor egestas. Morbi purus purus, efficitur in aliquet vitae, faucibus eros."
        }, "A.lparslan123").Wait();

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
