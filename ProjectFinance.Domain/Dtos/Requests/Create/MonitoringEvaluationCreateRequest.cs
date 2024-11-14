namespace ProjectFinance.Domain.Dtos.Requests;

public class MonitoringEvaluationCreateRequest
{
    public int? ActivityId { get; set; }

    public string? Note { get; set; }
    
    public string? workDone { get; set; }

    public int? ProjectId { get; set; }
}