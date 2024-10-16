namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonProjectRequest
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }
    
    public int? ProjectTypeId { get; set; }
}