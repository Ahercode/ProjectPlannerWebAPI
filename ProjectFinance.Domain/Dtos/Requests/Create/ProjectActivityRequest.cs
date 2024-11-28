namespace ProjectFinance.Domain.Dtos.Requests;

public class ProjectActivityRequest
{
    public int? ActivityId { get; set; }

    public int? ProjectId { get; set; }

    public string? Reference { get; set; }

    public int? ContractorId { get; set; }
    
    public decimal? Amount { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}