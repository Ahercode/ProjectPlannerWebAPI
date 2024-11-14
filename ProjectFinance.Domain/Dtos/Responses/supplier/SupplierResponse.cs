namespace ProjectFinance.Domain.Dtos.Responses.supplier;

public class SupplierResponse
{
    public int Id { get; set; }
    
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}