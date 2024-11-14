using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProjectFinance.Infrastructure.Repositories.Interfaces.UnitOfWork;

namespace ProjectFinance.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DashBoardController : ControllerBase
{
    private readonly IConfiguration _config;

    public DashBoardController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("projectPerActivity")]
    public async Task<IActionResult> GetDashBoardData()
    {
        var connectionString = _config.GetConnectionString("ProjectPlannerConnection");
        var query =
            "select [name], datediff(day,startdate,enddate) as duration, datediff(day, startdate, getdate()) as sofar  FROM [ProjectPlanner].[dbo].[Project]";

        var results = new List<object>();

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            connection.Open();
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    Name = reader["name"],
                    Duration = reader["duration"],
                    SoFar = reader["sofar"]
                });
            }
        }

        return Ok(results);
    }
}