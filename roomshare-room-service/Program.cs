using AutoMapper;
using Microsoft.EntityFrameworkCore;
using roomshare_room_service.Config;
using roomshare_room_service.Model.Context;
using roomshare_room_service.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
var userid = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
var productsdb = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";

builder.Services.AddDbContextPool<MySQLContext>(options =>
  options.UseMySql(mySqlConnStr,
      ServerVersion.AutoDetect(mySqlConnStr)));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();

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
