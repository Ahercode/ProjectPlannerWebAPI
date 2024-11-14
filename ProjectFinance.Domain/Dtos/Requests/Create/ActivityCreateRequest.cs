namespace ProjectFinance.Domain.Dtos.Requests;

public class ActivityCreateRequest
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}