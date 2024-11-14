namespace ProjectFinance.Domain.Dtos.Requests;

public class SupplierCreateRequest
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}