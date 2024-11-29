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
    
    [HttpGet("projectActivitySummary")]
    public async Task<IActionResult> GetDashBoardData2()
    {
        var connectionString = _config.GetConnectionString("ProjectPlannerConnection");
        var query =
            "select p.Name as ProjectName,  sum(pa.Amount) as TotalAmount, count(pa.Id) as TotalActivities FROM [ProjectPlanner].[dbo].[ProjectActivity] pa JOIN [ProjectPlanner].[dbo].[Project] P ON P.Id = pa.ProjectId GROUP BY p.Name";

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
                    ProjectName = reader["ProjectName"],
                    TotalAmount = reader["TotalAmount"],
                    TotalActivities = reader["TotalActivities"]
                });
            }
        }

        return Ok(results);
    }
    
    [HttpGet("activityTimeline")]
    public async Task<IActionResult> GetActivityTimeline()
    {
        var connectionString = _config.GetConnectionString("ProjectPlannerConnection");
        var query =
            " SELECT B.[name] AS ActivityName, MIN(A.startdate) AS MinStartDate, MAX(A.enddate) AS MaxEndDate, SUM(datediff(DAY, A.StartDate, A.EndDate)) as TotaldaysLeft from [ProjectPlanner].[dbo].[ProjectActivity] A join [ProjectPlanner].[dbo].[Activity] B on A.ActivityId=B.Id GROUP BY B.[name] ORDER BY B.[Name]";

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
                    ActivityName = reader["ActivityName"],
                    MinStartDate = reader.GetDateTime(reader.GetOrdinal("MinStartDate")).ToString("yyyy-MM-dd"),
                    MaxEndDate = reader.GetDateTime(reader.GetOrdinal("MaxEndDate")).ToString("yyyy-MM-dd"),
                    TotaldaysLeft = reader["TotaldaysLeft"]
                });
            }
        }

        return Ok(results);
    }
    
    [HttpGet("activityTimeline/{projectId}")]
    public async Task<IActionResult> GetActivityTimeline(int projectId)
    {
        var connectionString = _config.GetConnectionString("ProjectPlannerConnection");
        var query =
            "SELECT A.ProjectId, B.[name] ActivityName, A.startdate AS StartDate, A.enddate AS EndDate, datediff(day, A.StartDate, A.EndDate) AS TotalDaysLeft FROM [ProjectPlanner].[dbo].[ProjectActivity] A JOIN [ProjectPlanner].[dbo].[Activity] B on A.ActivityId=B.Id WHERE A.ProjectId=@projectId ORDER BY B.[Name],A.ProjectId";
        var results = new List<object>();

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@projectId", projectId);
            connection.Open();
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    ProjectId = reader["ProjectId"],
                    ActivityName = reader["ActivityName"],
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")).ToString("yyyy-MM-dd"),
                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")).ToString("yyyy-MM-dd"),
                    TotalDaysLeft = reader["TotalDaysLeft"]
                });
            }
        }

        return Ok(results);
    }
}