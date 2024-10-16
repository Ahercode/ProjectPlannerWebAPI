namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectScheduleRequest
{
    public int Id { get; set; }
    
    public int? ProjectId { get; set; }

    public DateTime? Date { get; set; }

}