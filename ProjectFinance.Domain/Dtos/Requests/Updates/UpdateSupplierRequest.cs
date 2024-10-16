namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateSupplierRequest
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Code { get; set; }
}