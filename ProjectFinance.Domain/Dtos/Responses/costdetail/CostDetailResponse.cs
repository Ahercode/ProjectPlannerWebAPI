namespace ProjectFinance.Domain.Dtos.Responses.costdetail;

public class CostDetailResponse
{
    public int Id { get; set; }
    
    public string? Code { get; set; }
    
    public string? Name { get; set; }
    public int? CostCategoryId { get; set; }
    public string? CostCategoryName { get; set; }
}