using System.Reflection;
using AutoMapper;
using BaseAPI.Core.Interfaces.Authentication;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Interfaces.UnitOfWork;
using BaseAPI.Core.Model;
using BaseAPI.Data;
using BaseAPI.Data.Repositories;
using BaseAPI.Data.UnitOfWorks;
using BaseAPI.Service.Mapping;
using BaseAPI.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(BaseService<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>();

builder.Services.Configure<Settings>(builder.Configuration.GetRequiredSection("Settings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(x =>
x.UseSqlServer(settings?.MsSQLConnection, option =>
{
    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
}
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

