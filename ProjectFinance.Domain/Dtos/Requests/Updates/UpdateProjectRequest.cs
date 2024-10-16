namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateProjectRequest
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }
    
    public int? ProjectTypeId { get; set; }
}