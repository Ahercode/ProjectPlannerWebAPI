namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class ActivityUpdateRequest
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}