using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Services;
using SmartKiwiApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartKiwiContext>(options => options.UseSqlServer("string"));
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<SmartKiwiContext>();
builder.Services.AddScoped<UserService>();
builder.Services.Configure<IdentityOptions>(options => 
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase =true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        
    });
