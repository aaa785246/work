using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MMGD.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<interviewContext>(
   options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtDatabase")));

builder.Services.AddCors(option => option.AddPolicy("any", builder =>
{
    builder.AllowAnyOrigin()//允許全網域
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("any");

app.MapControllers();

app.Run();