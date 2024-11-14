namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class CostDetailUpdateRequest
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? CostCategoryId { get; set; }
}