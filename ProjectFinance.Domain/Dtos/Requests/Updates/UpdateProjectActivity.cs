using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectActivity
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public int? ProjectId { get; set; }

    public string? Reference { get; set; }

    public int? ContractorId { get; set; }
    
    public decimal? Amount { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Note { get; set; }

    public string? FileName { get; set; }
    public IFormFile? FileObject { get; set; }
}