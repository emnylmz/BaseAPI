using System.Reflection;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Interfaces.UnitOfWork;
using BaseAPI.Data;
using BaseAPI.Data.Repositories;
using BaseAPI.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



builder.Services.AddDbContext<AppDbContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
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

