using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ProjectPlannerConnection");
builder.Services.AddDbContext<ProjectFinanceContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// configure cors
app.UseCors(options =>
{
    const string localHost = "http://localhost:3000";
    const string localHost2 = "http://localhost:5173";
    const string production2 = "https://app.sipconsult.net/";
    options
        .WithOrigins(localHost, localHost2, production2)
        .AllowAnyMethod()
        .AllowAnyHeader();
});

 app.UseSwagger();
 app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();