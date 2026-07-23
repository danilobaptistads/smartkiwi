using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Services;
using SmartKiwiApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartKiwiContext>(options => options.UseSqlServer("string"));
builder.Services.AddIdentityCore<User>(options => 
            {
                options.User.RequireUniqueEmail =true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase =true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                
            })
            .AddUserValidator<EmailValidator>()
            .AddUserValidator<CustomUserValidator>()
            .AddEntityFrameworkStores<SmartKiwiContext>();
builder.Services.AddScoped<UserService>();

