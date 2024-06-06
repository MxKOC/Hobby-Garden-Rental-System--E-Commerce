
using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("NonAdmin", policy => policy.RequireAssertion(context =>
            !context.User.IsInRole("admin")));
    });
builder.Services.AddDbContext<BlogContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDatabase")));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<IRequestRepository, EfRequestRepository>();
builder.Services.AddScoped<IMapRepository, EfMapRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IFieldRepository, EfFieldRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {options.LoginPath = "/Login/Login";});

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
app.UseAuthentication();
app.UseAuthorization();

app.UseCustomMiddleware();


app.UseHangfireDashboard();
SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "Rentmap",
    pattern: "{controller=Home}/{action=Index}/{userid?}/{fieldid?}");

app.MapControllerRoute(
    name: "Payment",
    pattern: "{controller=Home}/{action=Index}/{ShoppingCart?}/{fieldid?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
