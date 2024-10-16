namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectCategory
{
    public int Id { get; set; }
    
    public string? Code { get; set; }

    public string? Name { get; set; }
}