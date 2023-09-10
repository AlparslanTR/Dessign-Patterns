using Base.Main.Models;
using Base.Main.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using WebApp.DecoratorPattern.Decarators;
using WebApp.DecoratorPattern.Repositories;

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

builder.Services.AddHttpContextAccessor();

// Kütüphane ile birlikte kullaným þekli
//builder.Services.AddScoped<IProductRepository, ProductRepository>()
//    .Decorate<IProductRepository,ProductCacheDecorator>()
//    .Decorate<IProductRepository,ProductLogDecorator>()
//    ;


// Runtime esnasýn da ki deðiþiklikler için kullanýlabilecek yol.
builder.Services.AddScoped<IProductRepository>(Sp =>
{
    var context = Sp.GetRequiredService<AppDbContext>();
    var httpContextAccessor = Sp.GetRequiredService<IHttpContextAccessor>();
    var memoryCache = Sp.GetRequiredService<IMemoryCache>();
    var log = Sp.GetRequiredService<ILogger<ProductLogDecorator>>();
    var user = Sp.GetRequiredService<UserManager<Customer>>();
    var productRepository = new ProductRepository(context);

    // Adý Alparslan olanýn log kaydýný alma sadece cache iþlemi uygula. Runtime esnasýn da iþ görür.
    // Eðer her ikiside yapýlacaksa cachedecorator eklenmeli ardýndan productrepo yerine eklenmeli.
    // Ama bu ilerleyen projeler de büyüyebilir runtime müdahalesi olmayacaksa kütüphane ile kullanýlmasý önerilir.
    if (httpContextAccessor.HttpContext.User.Identity.Name is "Alparslan")
    {
        var cacheDecorator = new ProductCacheDecorator(productRepository, memoryCache);
        return cacheDecorator;
    }
    //var cacheDecorator = new ProductCacheDecorator(productRepository, memoryCache);
    var logDecarator = new ProductLogDecorator(productRepository, log, user);
    return logDecarator;
});

builder.Services.AddMemoryCache();
builder.Services.AddLogging();

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
