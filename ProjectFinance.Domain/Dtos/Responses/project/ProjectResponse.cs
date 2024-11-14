namespace ProjectFinance.Domain.Dtos.Responses.project;

public class ProjectResponse
{
    public int Id { get; set; }
    
    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? ProjectTypeId { get; set; }
    
    public string? ProjectTypeName { get; set; }

    public int? ProjectCategoryId { get; set; }
    
    public string? ProjectCategoryName { get; set; }

    public int? CurrencyId { get; set; }
    
    public string? CurrencyName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? ClientId { get; set; }
    
    public string? ClientName { get; set; }

    public decimal? ContractSum { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }
    
    public string? ContractorName { get; set; }

    public int? ProjectPriority { get; set; }
}