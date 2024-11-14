namespace ProjectFinance.Domain.Dtos.Responses;

public class StakeHolderResponse
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
    
    public string? Designation { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
    
    public int? ItemId { get; set; }
}