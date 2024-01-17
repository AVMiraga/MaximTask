using FluentValidation;
using MaximTask.Business.MapperProfiles;
using MaximTask.Business.Services.Implementation;
using MaximTask.Business.Services.Interfaces;
using MaximTask.Business.ViewModel.Account;
using MaximTask.Core.Entities;
using MaximTask.DAL.Context;
using MaximTask.DAL.Repositories.Implementation;
using MaximTask.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(
    opt=>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    }
);

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddAutoMapper(typeof(DefaultMapperProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<LoginVmValidator>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+";
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
