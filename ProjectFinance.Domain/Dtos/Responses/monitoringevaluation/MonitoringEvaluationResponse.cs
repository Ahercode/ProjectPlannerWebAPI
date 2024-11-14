namespace ProjectFinance.Domain.Dtos.Responses.monitoringevaluation;

public class MonitoringEvaluationResponse
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }
    
    public string? ActivityName { get; set; }
    
    public string? Email { get; set; }

    public string? Note { get; set; }
    
    public string? workDone { get; set; }

    public int? ProjectId { get; set; }
    
    public string? ProjectName { get; set; }
}