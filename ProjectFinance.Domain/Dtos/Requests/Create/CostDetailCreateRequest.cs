namespace ProjectFinance.Domain.Dtos.Requests;

public class CostDetailCreateRequest
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? CostCategoryId { get; set; }
}