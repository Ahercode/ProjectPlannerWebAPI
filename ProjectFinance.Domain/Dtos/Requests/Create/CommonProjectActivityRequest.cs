namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonProjectActivityRequest
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public int? ProjectId { get; set; }
}