using Microsoft.EntityFrameworkCore;
using ProjectFinance.Infrastructure.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ProjectPlannerConnection");
builder.Services.AddDbContext<ProjectFinanceContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // I want to describe the API with Swagger
    
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI( options => {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Finance API");
    //     options.RoutePrefix = string.Empty;
    // });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();