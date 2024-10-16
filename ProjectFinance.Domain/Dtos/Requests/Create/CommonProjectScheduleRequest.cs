namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonProjectScheduleRequest
{
    public int Id { get; set; }
    
    public int? ProjectId { get; set; }

    public DateTime? Date { get; set; }
}