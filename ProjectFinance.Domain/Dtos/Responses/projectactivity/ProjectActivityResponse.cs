namespace ProjectFinance.Domain.Dtos.Responses.projectactivity;

public class ProjectActivityResponse
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }
    
    public string? ActivityName { get; set; }

    public int? ProjectId { get; set; }
    
    public string? ProjectName { get; set; }

    public string? Reference { get; set; }

    public int? ContractorId { get; set; }
    
    public string? ContractorName { get; set; }
    
    public decimal? Amount { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}