namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectTypeRequest
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Code { get; set; }
}